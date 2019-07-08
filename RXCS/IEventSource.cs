namespace CSharpLibraries.RXCS
{
    public interface IEventSource
    {
        void NotifyObjectChanged(object obj, int prop);
    }
}
