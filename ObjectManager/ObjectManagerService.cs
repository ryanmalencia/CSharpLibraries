using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpLibraries.ObjectManager
{
    public class ObjectManagerService
    {
        private uint m_currentId = 1;
        private Dictionary<uint, SaveableObject> m_registeredObjects;
        private Dictionary<uint, Type> m_registeredTypes;
        public static ObjectManagerService Instance { get; } = new ObjectManagerService();

        private Dictionary<uint, SaveableObject> RegisteredObjects
        {
            get
            {
                return Instance.m_registeredObjects;
            }
            set
            {
                Instance.m_registeredObjects = value;
            }
        }

        private Dictionary<uint, Type> RegisteredTypes
        {
            get
            {
                return Instance.m_registeredTypes;
            }
            set
            {
                Instance.m_registeredTypes = value;
            }
        }

        private ObjectManagerService()
        {
            m_registeredObjects = new Dictionary<uint, SaveableObject>();
            m_registeredTypes = new Dictionary<uint, Type>();
        }

        static ObjectManagerService()
        {

        }

        public bool RegisterObject(SaveableObject obj)
        {
            if (obj.Id != 0)
                return false;

            if (!Attribute.IsDefined(obj.GetType(), typeof(SerializableAttribute)))
                return false;

            obj.Id = m_currentId++;
            RegisteredObjects.Add(m_currentId, obj);

            if(RegisteredTypes.TryGetValue(obj.ClassId, out Type val))
            {
                if (val != obj.GetType())
                    throw new ArgumentException();
            }
            else
            {
                RegisteredTypes.Add(obj.ClassId, obj.GetType());
            }
            
            return true;
        }

        public bool UnregisterObject(SaveableObject obj)
        {
            return RegisteredObjects.Remove(obj.Id);
        }
    
        public void Save(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, RegisteredTypes);
            stream.Write(BitConverter.GetBytes(RegisteredObjects.Count), 0, sizeof(int));
            foreach (var saveablePair in RegisteredObjects)
            {
                saveablePair.Value.Save(stream);
            }
        }

        public static object ResumeObject(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bClassId = new byte[sizeof(uint)];
            stream.Read(bClassId, 0, sizeof(uint));
            uint classId = BitConverter.ToUInt32(bClassId, 0);
            return bf.Deserialize(stream);
        }

        public void Resume(Stream stream)
        {
            RegisteredTypes.Clear();
            RegisteredObjects.Clear();
            BinaryFormatter bf = new BinaryFormatter();
            RegisteredTypes = (Dictionary <uint, Type>)bf.Deserialize(stream);
            byte[] aCount = new byte[sizeof(int)];
            stream.Read(aCount, 0, sizeof(int));
            int count = BitConverter.ToInt32(aCount, 0);
            int i = 0;
            while(i < count)
            {
                SaveableObject so = ResumeObject(stream) as SaveableObject;
                RegisteredObjects.Add(so.Id, so);
                ++i;
            }
        }
    }
}
