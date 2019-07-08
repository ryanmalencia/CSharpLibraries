namespace CSharpLibraries.RXCS
{
    interface IEventSink
    {
        void OnObjectChanged(object sender, ObjectChangedEventArgs e);
    }
}
