namespace CSharpLibraries.Generics.Interfaces
{
    interface IList<T>
    {
        void Add(T item);
        T Remove(T item);
        void Insert(int index, T item);
        bool Contains(T item);
        void Clear();
        int Size();
    }
}
