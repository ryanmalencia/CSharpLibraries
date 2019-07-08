namespace CSharpLibraries.Generics.Interfaces
{
    interface IStack<T>
    {
        void Push(T item);
        T Pop();
        T Peek();
        bool IsEmpty();
        void Clear();
    }
}
