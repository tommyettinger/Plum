﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Plum
{
    class PlumDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        class KeyCollection<TKey> : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
        {
            internal KeyCollection(PlumDictionary<TKey, TValue> outer)
            {
                o = outer;
            }
        

        
            internal PlumDictionary<TKey, TValue> o;
            public int Count
            {
                get { return o.Pit.Keys.Count(); }
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                return o.Pit.Keys.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)(o.Pit.Keys)).GetEnumerator();
            }

            public void Add(TKey item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TKey item)
            {
                return o.Pit.Keys.Contains(item);
            }

            public void CopyTo(TKey[] array, int arrayIndex)
            {
                o.Pit.Keys.ToArray().CopyTo(array, arrayIndex);
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(TKey item)
            {
                throw new NotSupportedException();
            }
        }

        internal ImmutableDictionary<TKey, TValue> Pit;


        // Summary:
        //     Gets an System.Collections.Generic.ICollection<T> containing the keys of
        //     the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     An System.Collections.Generic.ICollection<T> containing the keys of the object
        //     that implements System.Collections.Generic.IDictionary<TKey,TValue>.
        ICollection<TKey> Keys { get; }
        //
        // Summary:
        //     Gets an System.Collections.Generic.ICollection<T> containing the values in
        //     the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     An System.Collections.Generic.ICollection<T> containing the values in the
        //     object that implements System.Collections.Generic.IDictionary<TKey,TValue>.
        ICollection<TValue> Values { get; }

        // Summary:
        //     Gets or sets the element with the specified key.
        //
        // Parameters:
        //   key:
        //     The key of the element to get or set.
        //
        // Returns:
        //     The element with the specified key.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.Collections.Generic.KeyNotFoundException:
        //     The property is retrieved and key is not found.
        //
        //   System.NotSupportedException:
        //     The property is set and the System.Collections.Generic.IDictionary<TKey,TValue>
        //     is read-only.
        TValue this[TKey key] { get; set; }

        // Summary:
        //     Adds an element with the provided key and value to the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Parameters:
        //   key:
        //     The object to use as the key of the element to add.
        //
        //   value:
        //     The object to use as the value of the element to add.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.ArgumentException:
        //     An element with the same key already exists in the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
        void Add(TKey key, TValue value);
        //
        // Summary:
        //     Determines whether the System.Collections.Generic.IDictionary<TKey,TValue>
        //     contains an element with the specified key.
        //
        // Parameters:
        //   key:
        //     The key to locate in the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Returns:
        //     true if the System.Collections.Generic.IDictionary<TKey,TValue> contains
        //     an element with the key; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        bool ContainsKey(TKey key);
        //
        // Summary:
        //     Removes the element with the specified key from the System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Parameters:
        //   key:
        //     The key of the element to remove.
        //
        // Returns:
        //     true if the element is successfully removed; otherwise, false. This method
        //     also returns false if key was not found in the original System.Collections.Generic.IDictionary<TKey,TValue>.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        //
        //   System.NotSupportedException:
        //     The System.Collections.Generic.IDictionary<TKey,TValue> is read-only.
        bool Remove(TKey key);
        //
        // Summary:
        //     Gets the value associated with the specified key.
        //
        // Parameters:
        //   key:
        //     The key whose value to get.
        //
        //   value:
        //     When this method returns, the value associated with the specified key, if
        //     the key is found; otherwise, the default value for the type of the value
        //     parameter. This parameter is passed uninitialized.
        //
        // Returns:
        //     true if the object that implements System.Collections.Generic.IDictionary<TKey,TValue>
        //     contains an element with the specified key; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     key is null.
        bool TryGetValue(TKey key, out TValue value);
    }
}
