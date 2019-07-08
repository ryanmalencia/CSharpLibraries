using System.Collections.Generic;

namespace CSharpLibraries.Generics
{
    public class ArrayBag<T> : IEnumerable<T>, Generics.Interfaces.IBag<T>
    {
        private readonly T[] _bag;
        private static readonly int DEFAULT_CAPACITY = 50;
        private int _numberOfEntries = 0;
        private int _capacity = DEFAULT_CAPACITY;

        public ArrayBag() : this(DEFAULT_CAPACITY)
        {
            
        }

        public ArrayBag(int capacity)
        {
            _bag = new T[capacity];
            _capacity = capacity;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            int currentIdx = 0;
            while (currentIdx < _numberOfEntries)
            {
                yield return _bag[currentIdx];
                ++currentIdx;
            }
        }

        /// <summary>
        /// Get number of items in Bag.
        /// </summary>
        /// <returns>Number of items in Bag.</returns>
        public int Size()
        {
            return this.Count;
        }

        /// <summary>
        /// Get number of items in Bag.
        /// </summary>
        public int Count
        {
            get
            {
                return _numberOfEntries;
            }
        }

        /// <summary>
        /// Get if Bag is full.
        /// </summary>
        public bool IsFull
        {
            get
            {
                return _numberOfEntries == _capacity;
            }
        }

        /// <summary>
        /// Get if Bag is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _numberOfEntries == 0;
            }
        }

        /// <summary>
        /// Add item to Bag.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>True if item successfully added.</returns>
        public bool Add(T item)
        {
            if (item != null && !this.IsFull)
            {
                _bag[_numberOfEntries++] = item;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove all items from Bag.
        /// </summary>
        public void Clear()
        {
            while (!this.IsEmpty) Remove();
        }

        /// <summary>
        /// Remove an item from Bag.
        /// </summary>
        /// <returns>Removed item.</returns>
        public T Remove()
        {
            T removedItem = default;

            if (!this.IsEmpty)
            {
                removedItem = _bag[_numberOfEntries - 1];
                _bag[_numberOfEntries - 1] = default;
                --_numberOfEntries;
            }

            return removedItem;
        }

        /// <summary>
        /// Get item at index.
        /// </summary>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index.</returns>
        public T this[int index]
        {
            get
            {
                if (index >= _numberOfEntries || index < 0)
                    return default;

                return _bag[index];
            }
        }

        /// <summary>
        /// Remove specified item from Bag.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Removed item.</returns>
        public T Remove(T item)
        {
            T removedItem = default;

            int idx = this.getIndexOf(item);
            if(idx > -1)
            {
                removedItem = _bag[idx];
                _bag[idx] = _bag[_numberOfEntries - 1];
                _bag[_numberOfEntries - 1] = default;
                --_numberOfEntries;
            }

            return removedItem;
        }

        /// <summary>
        /// Check if item is present in Bag.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>True if item is found.</returns>
        public bool Contains(T item)
        {
            return getIndexOf(item) > -1;
        }

        private int getIndexOf(T item)
        {
            int index = -1;

            for(int idx = 0; idx < _numberOfEntries; ++idx)
            {
                if (_bag[idx].Equals(item))
                {
                    index = idx;
                    break;
                }
            }

            return index;
        }
    }
}
