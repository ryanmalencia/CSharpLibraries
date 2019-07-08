namespace CSharpLibraries.Generics.Interfaces
{
    interface IBag<T>
    {
        bool Add(T item);
        void Clear();
        T Remove();
        T Remove(T item);
        bool Contains(T item);
        int Size();
    }
}
