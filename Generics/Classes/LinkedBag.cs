using System.Collections.Generic;

namespace CSharpLibraries.Generics
{
    public class LinkedBag<T> : IEnumerable<T>, Generics.Interfaces.IBag<T>
    {
        private List<T> _bag;

        private int _numberOfEntries = 0;

        public LinkedBag()
        {
            _bag = new List<T>();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in _bag)
            {
                yield return item;
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
        /// Remove all items from bag. 
        /// </summary>
        public void Clear()
        {
            _bag.Clear();
        }

        /// <summary>
        /// Add item to bag.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>True if item added.</returns>
        public bool Add(T item)
        {
            _bag.Add(item);
            return true;
        }

        /// <summary>
        /// Remove an item from the bag.
        /// </summary>
        /// <returns>The removed item.</returns>
        public T Remove()
        {
            return _bag.Remove();
        }

        /// <summary>
        /// Remove specified item from bag.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Removed item.</returns>
        public T Remove(T item)
        {
            return _bag.Remove(item);
        }

        /// <summary>
        /// Check if item is in bag.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>True if item is present in bag.</returns>
        public bool Contains(T item)
        {
            return _bag.Contains(item);
        }
    }
}
