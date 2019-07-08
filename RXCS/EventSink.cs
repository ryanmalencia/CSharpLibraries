using CSharpLibraries.BaseObjects;

namespace CSharpLibraries.RXCS
{
    public abstract class EventSink : BaseObject, IEventSink
    {
        public virtual void OnObjectChanged(object sender, ObjectChangedEventArgs e)
        {

        }

        public EventSink(uint classId) : base (classId)
        {
            EventService.Instance.RegisterSink(this);
        }

        ~EventSink()
        {
            EventService.Instance.UnregisterSink(this.Id);
        }
    }
}
