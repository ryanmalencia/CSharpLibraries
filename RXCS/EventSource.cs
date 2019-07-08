using CSharpLibraries.BaseObjects;
using System.Linq;

namespace CSharpLibraries.RXCS
{
    public abstract class EventSource : BaseObject, IEventSource
    {
        public delegate void ObjectChangedHandler(object sender, ObjectChangedEventArgs e);
        private ObjectChangedHandler m_onObjectChanged;
        public event ObjectChangedHandler OnObjectChanged
        {
            add
            {
                if (m_onObjectChanged == null || !m_onObjectChanged.GetInvocationList().Contains(value))
                    m_onObjectChanged += value;
            }
            remove
            {
                m_onObjectChanged -= value;
            }
        }

        public void NotifyObjectChanged(object obj, int prop)
        {
            this.SendObjectChangedEvent(obj, prop);
        }

        public void SendObjectChangedEvent(object obj, int prop)
        {
            if (m_onObjectChanged == null) return;

            ObjectChangedEventArgs args = new ObjectChangedEventArgs(obj, prop);
            this.m_onObjectChanged(obj, args);
        }

        public EventSource(uint classId) : base(classId)
        {
            EventService.Instance.RegisterSource(this);
        }

        ~EventSource()
        {
            if(Id != 0)
            {
                EventService.Instance.UnregisterSource(Id);
            }
        }
    }
}
