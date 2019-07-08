using System;

namespace CSharpLibraries.Generics
{
    class DNode<T>
    {
        private DNode<T> _nextNode;
        private DNode<T> _prevNode;
        private readonly T _data;

        public DNode(T data)
        {
            if (data == null)
                throw new ArgumentNullException("Cannot create Node with null data.");
            _data = data;
        }

        public void AddLink(DNode<T> nextNode)
        {
            _nextNode = nextNode ?? throw new ArgumentNullException("Cannot create Node with null data.");
            _nextNode._prevNode = this;
        }

        public DNode<T> NextNode
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

        public DNode<T> PrevNode
        {
            get
            {
                return _prevNode;
            }
            set
            {
                _prevNode = value;
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
