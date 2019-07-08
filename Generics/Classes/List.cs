using System;
using System.Collections.Generic;

namespace CSharpLibraries.Generics
{
    public class List<T> : IEnumerable<T>, Generics.Interfaces.IList<T>
    {
        private DNode<T> _root = null;
        private DNode<T> _last = null;

        public List()
        {

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            DNode<T> currentNode = _root;
            while(currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.NextNode;
            }
        }

        /// <summary>
        /// Get number of items in List
        /// </summary>
        /// <returns>Number of items.</returns>
        public int Size()
        {
            return this.Count;
        }

        /// <summary>
        /// Get number of items in List
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;

                DNode<T> currentNode = _root;
                while(currentNode != null)
                {
                    currentNode = currentNode.NextNode;
                    ++count;
                }

                return count;
            }
        }

        /// <summary>
        /// Add new item to end of List.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(T item)
        {
            DNode<T> newNode = new DNode<T>(item);
            if (_root == null)
                _root = newNode;
            else
                _last.AddLink(newNode);
            _last = newNode;
        }

        /// <summary>
        /// Remove item from List.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Removed item. Null if item is not found.</returns>
        public T Remove(T item)
        {
            DNode<T> currentNode = _root;
            while (currentNode != null)
            {
                if (item.Equals(currentNode.Data))
                {
                    currentNode.PrevNode.NextNode = currentNode.NextNode;
                    currentNode.NextNode.PrevNode = currentNode.PrevNode;
                    return currentNode.Data;
                }
                currentNode = currentNode.NextNode;
            }
            return default;  
        }

        /// <summary>
        /// Remove last item from list.
        /// </summary>
        /// <returns>Removed item.</returns>
        public T Remove()
        {
            T data = default;

            if(Count == 1)
            {
                data = _last.Data;
                _last = null;
                _root = null;
                return data;
            }
            else if(Count > 1)
            {
                data = _last.Data;
                _last.PrevNode.NextNode = null;
                _last = _last.PrevNode;
            }
            
            return data;
        }

        /// <summary>
        /// Insert item at given index.
        /// </summary>
        /// <param name="index">Index to insert item.</param>
        /// <param name="item">Item to insert.</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException();

            int count = 0;
            DNode<T> currentNode = _root;
            while(currentNode != null)
            {
                if(count == index)
                {
                    DNode<T> newNode = new DNode<T>(item)
                    {
                        PrevNode = currentNode.PrevNode,
                        NextNode = currentNode
                    };
                    currentNode.PrevNode.NextNode = newNode;
                    currentNode.PrevNode = newNode;
                    return;
                }
                currentNode = currentNode.NextNode;
                ++count;
            }
            this.Add(item);
        }

        /// <summary>
        /// Returns whether an item is already in List.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>True if item is present in List.</returns>
        public bool Contains(T item)
        {
            DNode<T> currentNode = _root;
            while(currentNode != null)
            {
                if (item.Equals(currentNode.Data))
                    return true;
                currentNode = currentNode.NextNode;
            }
            return false;
        }

        /// <summary>
        /// Get last item in list.
        /// </summary>
        /// <returns>Last item in list.</returns>
        public T Last()
        {
            T data = default;

            if (_last != null)
                data = _last.Data;

            return data;
        }

        /// <summary>
        /// Remove all items from List.
        /// </summary>
        public void Clear()
        {
            _root = null;
            _last = null;
        }

        /// <summary>
        /// Get item at given index.
        /// </summary>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index.</returns>
        public T this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0)
                    return default;

                int count = 0;
                DNode<T> currentNode = _root;
                while (currentNode != null)
                {
                    if (count == index)
                        return currentNode.Data;

                    currentNode = currentNode.NextNode;
                    ++count;
                }

                return default;
            }
        }
    }
}
