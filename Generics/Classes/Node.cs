using System;

namespace CSharpLibraries.Generics
{
    class Node<T>
    {
        private Node<T> _nextNode;
        private readonly T _data;

        public Node(T data)
        {
            if (data == null)
                throw new ArgumentNullException("Cannot create Node with null data.");
            _data = data;
        }

        public void AddLink(Node<T> nextNode)
        {
            _nextNode = nextNode ?? throw new ArgumentNullException("Cannot create Node with null data.");
        }

        public Node<T> NextNode
        {
            get
            {
                return _nextNode;
            }
            set
            {
                _nextNode = value;
            }
        }

        public T Data
        {
            get
            {
                return _data;
            }
        }
    }
}
