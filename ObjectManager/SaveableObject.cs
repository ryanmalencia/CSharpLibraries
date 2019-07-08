using CSharpLibraries.BaseObjects;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpLibraries.ObjectManager
{
    [Serializable]
    public class SaveableObject : BaseObject, ISaveable, IResumeable
    {
        public SaveableObject(uint classId) : base(classId)
        {

        }

        public virtual void Save(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                stream.Write(BitConverter.GetBytes(this.ClassId), 0, sizeof(uint));
                bf.Serialize(stream, this);
            }
            catch
            {

            }
        }

        public virtual void Resume(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bClassId = new byte[sizeof(uint)];
            stream.Read(bClassId, 0, sizeof(uint));
            uint classId = BitConverter.ToUInt32(bClassId, 0);
            object obj = bf.Deserialize(stream);
        }
    }
}
