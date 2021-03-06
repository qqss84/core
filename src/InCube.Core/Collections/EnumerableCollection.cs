﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static InCube.Core.Preconditions;

namespace InCube.Core.Collections
{
    /// <summary>
    /// Wraps an <see cref="IEnumerable{T}"/> into a read only <see cref="ICollection{T}"/> by adding a count argument.
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public class EnumerableCollection<T> : ICollection<T>, IReadOnlyCollection<T>
#pragma warning restore SA1402 // File may only contain a single type
    {
        public EnumerableCollection(IEnumerable<T> enumerable, int count)
        {
            this.enumerable = enumerable;
            Count = count;
        }

        public IEnumerator<T> GetEnumerator() => 
            this.enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item) => 
            this.enumerable.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            CheckArgument(Count <= array.Length - arrayIndex,
                "insufficient space for {0} elements in array of length {1} at index {2}",
                Count,
                array.Length,
                arrayIndex);
            var i = -1;
            foreach (var item in enumerable)
            {
                array[++i] = item;
            }
        }

        public bool Remove(T item) => 
            throw new NotSupportedException();

        public int Count { get; }

        public bool IsReadOnly => 
            true;

        private readonly IEnumerable<T> enumerable;
    }

    public static class EnumerableCollection
    {
        public static EnumerableCollection<T> ToCollection<T>(this IEnumerable<T> enumerable, int count) =>
            new EnumerableCollection<T>(enumerable, count);
    }
}