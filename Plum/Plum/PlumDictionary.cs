using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Plum
{
    /// <summary>
    /// A mutable IDictionary that wraps an immutable collection in order to gain structural sharing with other PlumDictionaries.
    /// </summary>
    /// <typeparam name="TKey">The type of keys for the PlumDictionary. Should be immutable.</typeparam>
    /// <typeparam name="TValue">The type of values for the PlumDictionary. Should be immutable.</typeparam>
    class PlumDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public class KeyCollection<TK> : ICollection<TK>, ICollection, IReadOnlyCollection<TK>
        {
            internal KeyCollection(PlumDictionary<TK, TValue> outer)
            {
                o = outer;
            }

            internal PlumDictionary<TK, TValue> o;
            public void CopyTo(Array array, int index)
            {
                o.Pit.Keys.ToArray().CopyTo(array, index);
            }

            public int Count
            {
                get { return o.Pit.Keys.Count(); }
            }

            public bool IsSynchronized { get; private set; }
            public object SyncRoot { get; private set; }

            public IEnumerator<TK> GetEnumerator()
            {
                return o.Pit.Keys.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)(o.Pit.Keys)).GetEnumerator();
            }

            public void Add(TK item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TK item)
            {
                return o.Pit.Keys.Contains(item);
            }

            public void CopyTo(TK[] array, int arrayIndex)
            {
                o.Pit.Keys.ToArray().CopyTo(array, arrayIndex);
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(TK item)
            {
                throw new NotSupportedException();
            }
        }
        public class ValueCollection<TV> : ICollection<TV>, ICollection, IReadOnlyCollection<TV>
        {
            internal ValueCollection(PlumDictionary<TKey, TV> outer)
            {
                o = outer;
            }

            internal PlumDictionary<TKey, TV> o;
            public void CopyTo(Array array, int index)
            {
                o.Pit.Values.ToArray().CopyTo(array, index);
            }

            public int Count
            {
                get { return o.Pit.Values.Count(); }
            }

            public bool IsSynchronized { get; private set; }
            public object SyncRoot { get; private set; }

            public IEnumerator<TV> GetEnumerator()
            {
                return o.Pit.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)(o.Pit.Values)).GetEnumerator();
            }

            public void Add(TV item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TV item)
            {
                return o.Pit.Values.Contains(item);
            }

            public void CopyTo(TV[] array, int arrayIndex)
            {
                o.Pit.Values.ToArray().CopyTo(array, arrayIndex);
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(TV item)
            {
                throw new NotSupportedException();
            }
        }

        internal ImmutableDictionary<TKey, TValue> Pit;

        private readonly KeyCollection<TKey> _keys;
        private readonly ValueCollection<TValue> _values;

        /// <summary>
        /// Constructs an empty PlumDictionary.
        /// </summary>
        public PlumDictionary()
        {
            Pit = ImmutableDictionary<TKey, TValue>.Empty;
            _keys = new KeyCollection<TKey>(this);
            _values = new ValueCollection<TValue>(this);
        }
        /// <summary>
        /// Constructs a PlumDictionary from an existing IEnumerable of keys and values (such as a Dictionary).
        /// </summary>
        /// <param name="coll">An IEnumerable of TKey, TValue; a Dictionary will work.</param>
        public PlumDictionary(IEnumerable<KeyValuePair<TKey, TValue>> coll)
        {
            Pit = ImmutableDictionary<TKey, TValue>.Empty.AddRange(coll);
            _keys = new KeyCollection<TKey>(this);
            _values = new ValueCollection<TValue>(this);
        }
        
        /// <summary>
        /// Gets an System.Collections.Generic.ICollection of TKey containing the keys of
        ///     the PlumDictionary.
        /// </summary>
        public ICollection<TKey> Keys { get { return _keys; } }

        /// <summary>
        /// Gets an System.Collections.Generic.ICollection of TValue containing the values in
        ///     the PlumDictionary.
        /// </summary>
        public ICollection<TValue> Values { get { return _values; } }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        public TValue this[TKey key] { get { return Pit[key]; } set { Pit = Pit.SetItem(key, value); } }

        /// <summary>
        /// Adds an element with the provided key and value to the PlumDictionary.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.ArgumentException">An element with the same key already exists in the PlumDictionary.</exception>
        public void Add(TKey key, TValue value)
        {
            Pit = Pit.Add(key, value);
        }

        /// <summary>
        /// Adds an IEnumerable of TKey,TValue (such as a Dictionary) to the PlumDictionary.
        /// </summary>
        /// <param name="coll">The IEnumerable of TKey,TValue to add to this PlumDictionary.</param>
        /// <exception cref="System.ArgumentException">An element with the same key as one of the keys in coll already exists in the PlumDictionary.</exception>
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> coll)
        {
            Pit = Pit.AddRange(coll);
        }

        /// <summary>
        /// Adds or updates this PlumDictionary with the key,value pairs in an IEnumerable of TKey,TValue (such as a Dictionary).
        /// </summary>
        /// <param name="coll">The IEnumerable of TKey,TValue to update this PlumDictionary with.</param>
        /// <exception cref="System.ArgumentException">An element with the same key as one of the keys in coll already exists in the PlumDictionary.</exception>
        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> coll)
        {
            Pit = Pit.SetItems(coll);
        }

        /// <summary>
        /// Determines whether the PlumDictionary
        ///     contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>true if the key is present, false if it is not.</returns>
        public bool ContainsKey(TKey key)
        {
            return Pit.ContainsKey(key);
        }
        /// <summary>
        /// Determines whether the PlumDictionary
        ///     contains one or more elements with the specified value.
        /// </summary>
        /// <param name="key">The value to check</param>
        /// <returns>true if the value is present, false if it is not.</returns>
        public bool ContainsValue(TValue val)
        {
            return Pit.ContainsValue(val);
        }

        /// <summary>
        /// Removes the element with the specified key from the PlumDictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public void Remove(TKey key)
        {
            Pit = Pit.Remove(key);
        }

        /// <summary>
        /// Removes all elements from the PlumDictionary.
        /// </summary>
        public void Clear()
        {
            Pit = Pit.Clear();
        }
        
        /// <summary>
        /// Removes the element with the specified key from the System.Collections.Generic.IDictionary of TKey,TValue;
        /// this is an explicit interface member implementation.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element was present before and is not present after this method returns; false if either
        /// there was no element to remove with the specified key, or if the removal failed.</returns>
        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            bool ret = Pit.ContainsKey(key);
            Pit = Pit.Remove(key);
            return ret && !Pit.ContainsKey(key);
        }
        
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if
        /// the key is found; otherwise, the default value for the type of the value
        /// parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the PlumDictionary
        /// contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Pit.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
