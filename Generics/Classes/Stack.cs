using System.Collections.Generic;

namespace CSharpLibraries.Generics
{
    public class Stack<T> : IEnumerable<T>, Generics.Interfaces.IStack<T>
    {
        private List<T> _stack;

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            int count = _stack.Count;
            for(int idx = count; idx > 0; --idx)
            {
                yield return _stack[idx - 1];
            }
        }

        public Stack()
        {
            _stack = new List<T>();
        }

        /// <summary>
        /// Push item to top of stack.
        /// </summary>
        /// <param name="item">Item to push.</param>
        public void Push(T item)
        {
            _stack.Add(item);
        }

        /// <summary>
        /// Remove top item from stack.
        /// </summary>
        /// <returns>Top item of stack.</returns>
        public T Pop()
        {
            return _stack.Remove();
        }

        /// <summary>
        /// Get top item on stack.
        /// </summary>
        /// <returns>Top item on stack.</returns>
        public T Peek()
        {
            return _stack.Last();
        }

        /// <summary>
        /// Get whether the stack is empty. 
        /// </summary>
        /// <returns>True if empty.</returns>
        public bool IsEmpty()
        {
            return _stack.Count == 0;
        }

        /// <summary>
        /// Remove all items from stack.
        /// </summary>
        public void Clear()
        {
            _stack.Clear();
        }
    }
}
