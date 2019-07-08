using System;

namespace CSharpLibraries.RXCS
{
    public class ObjectChangedEventArgs : EventArgs
    {
        public object ObjectChanged { get; private set; }
        public int PropertyChanged { get; private set; }

        public ObjectChangedEventArgs(object objectChanged, int propertyChanged)
        {
            this.ObjectChanged = objectChanged;
            this.PropertyChanged = propertyChanged;
        }
    }
}
    