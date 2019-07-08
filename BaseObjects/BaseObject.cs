using System;

namespace CSharpLibraries.BaseObjects
{
    [Serializable]
    public class BaseObject: IComparable<BaseObject>
    {
        protected uint m_id = 0;
        public readonly uint ClassId = 0;

        public BaseObject(uint classId)
        {
            ClassId = classId;
        }

        public uint Id
        {
            get
            {
                return m_id;
            }
            set
            {
                if (m_id == 0 && value != 0)
                    m_id = value;
            }
        }

        public int CompareTo(BaseObject other)
        {
            return this.m_id.CompareTo(other.Id);
        }
    }
}
