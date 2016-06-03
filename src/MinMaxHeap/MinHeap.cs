﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MinMaxHeap
{
    public class MinHeap<T>
    {
        List<T> values;
        IComparer<T> comparer;

        public MinHeap(IEnumerable<T> items, IComparer<T> comparer)
        {
            values = new List<T>();
            this.comparer = comparer;
            values.Add(default(T));
            values.AddRange(items);

            for (int i = values.Count / 2; i >= 1; i--)
            {
                bubbleDown(i);
            }
        }

        public MinHeap(IEnumerable<T> items)
            : this(items, Comparer<T>.Default)
        { }

        public MinHeap(IComparer<T> comparer)
            : this(new T[0], comparer)
        { }

        public MinHeap() : this(Comparer<T>.Default)
        { }

        public int Count
        {
            get

            {
                return values.Count - 1;
            }
        }

        public T Min
        {
            get
            {
                return values[1];
            }
        }

        /// <summary>
        /// Extract the smallest element.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public T ExtractMin()
        {
            int count = Count;

            if (count == 0)
            {
                throw new InvalidOperationException("Heap is empty.");
            }

            var min = Min;
            values[1] = values[count];
            values.RemoveAt(count);

            if (values.Count > 1)
            {
                bubbleDown(1);
            }

            return min;
        }

        /// <summary>
        /// Insert the key and value.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Add(T item)
        {
            values.Add(item);
            bubbleUp(Count);
        }

        private void bubbleUp(int index)
        {
            int parent = index / 2;

            while (
                index > 1 &&
                compareResult(parent, index) > 0)
            {
                exchange(index, parent);
                index = parent;
                parent = index / 2;
            }
        }

        private void bubbleDown(int index)
        {
            int min;

            while (true)
            {
                int left = index * 2;
                int right = index * 2 + 1;

                if (left < values.Count &&
                    compareResult(left, index) < 0)
                {
                    min = left;
                }
                else
                {
                    min = index;
                }

                if (right < values.Count &&
                    compareResult(right, min) < 0)
                {
                    min = right;
                }

                if (min != index)
                {
                    exchange(index, min);
                }
                else
                {
                    return;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int compareResult(int index1, int index2)
        {
            return comparer.Compare(values[index1], values[index2]);
        }

        private void exchange(int index, int max)
        {
            var tmp = values[index];
            values[index] = values[max];
            values[max] = tmp;
        }
    }
}
