using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Generic linked list class. Used for storing a list of values.
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    public class LinkedList<T> : IEnumerable<T>, IComparable<LinkedList<T>>, IEquatable<LinkedList<T>>
        where T : IComparable<T>, IEquatable<T>
    {
        class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data, Node next = null)
            {
                Data = data;
                Next = next;
            }
        }

        private Node head;
        private Node tail;

        /// <summary>
        /// Creates an empty linked list
        /// </summary>
        public LinkedList()
        {
        }

        /// <summary>
        /// Creates an clone of a given enumerable value/list
        /// </summary>
        /// <param name="other">Cloned enumerable</param>
        public LinkedList(IEnumerable<T> other)
        {
            foreach (var value in other)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Add a single element to the linked list
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            Node node = new Node(value);
            if (tail != null && head != null)
            {
                tail.Next = node;
                tail = node;
            }
            else
            {
                tail = node;
                head = node;
            }
        }

        /// <summary>
        /// Removes the last element from this list
        /// </summary>
        /// <returns>The last elements values or the default value if empty</returns>
        public T RemoveLast()
        {
            if (head == null)
            {
                return default;
            }

            Node prev = null;
            Node current = head;
            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            if (prev != null)
            {
                prev.Next = null;
                tail = prev;
            }

            if (prev == null)
            {
                head = null;
                tail = null;
            }

            return current.Data;
        }

        /// <summary>
        /// Gets the first element
        /// </summary>
        /// <returns>The first element</returns>
        public T GetFirst()
        {
            return head != null ? head.Data : default;
        }

        /// <summary>
        /// Gets the first element
        /// </summary>
        /// <returns>The first element</returns>
        public T GetLast()
        {
            return tail != null ? tail.Data : default;
        }

        /// <summary>
        /// Count how many elemnts are stored in this list
        /// </summary>
        /// <returns>Number of stored elements</returns>
        public int Count()
        {
            int count = 0;
            for (Node d = head; d != null; d = d.Next)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Returns true if the linked list is empty,
        /// what did you expect?
        /// </summary>
        /// <returns>A boolean</returns>
        public bool IsEmpty()
        {
            return head == null;
        }

        /// <summary>
        /// Sorts this linked list
        /// </summary>
        public void Sort()
        {
            for (Node nodeA = head; nodeA != null; nodeA = nodeA.Next)
            {
                Node min = nodeA;
                for (Node nodeB = nodeA.Next; nodeB != null; nodeB = nodeB.Next)
                {
                    if (nodeB.Data.CompareTo(min.Data) > 0)
                    {
                        min = nodeB;
                    }
                }

                T tmp = nodeA.Data;
                nodeA.Data = min.Data;
                min.Data = tmp;
            }
        }

        /// <summary>
        /// A human-readable string for debuging purposes
        /// </summary>
        /// <returns>Debug string</returns>
        public override string ToString()
        {
            return string.Format("LinkedList<{0}>( Count = {0} )", Count());
        }

        /// <summary>
        /// Enumerate over all the elements of the list
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (Node d = head; d != null; d = d.Next)
                yield return d.Data;
        }

        /// <summary>
        /// Enumerate over all the elements of the list
        /// </summary>
        /// <returns>An enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Compares the ordering 2 lists
        /// </summary>
        /// <param name="other">Other list</param>
        /// <returns></returns>
        public int CompareTo(LinkedList<T> other)
        {
            var otherEnumerator = other.GetEnumerator();
            foreach (var value in this)
            {
                if (otherEnumerator.MoveNext())
                {
                    int comparison = value.CompareTo(otherEnumerator.Current);
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                } else
                {
                    return -1;
                }
            }

            if (otherEnumerator.MoveNext())
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Checks if 2 linked lists have the same values stored
        /// </summary>
        /// <param name="other">Other linked list</param>
        /// <returns>True if they both store the same values</returns>
        public bool Equals(LinkedList<T> other)
        {
            var otherEnumerator = other.GetEnumerator();
            foreach (var value in this)
            {
                if (otherEnumerator.MoveNext())
                {
                    if (!value.Equals(otherEnumerator.Current))
                    {
                        // This is the case, when some elements in between the lists are different
                        return false;
                    }
                }
                else
                {
                    // This is the case when the other linked list runs out of values, while this still has more.
                    return false;
                }
            }

            if (otherEnumerator.MoveNext())
            {
                // This is the case, when the other linked list still had some left over values.
                // That means it had more elements that this one.
                return false;
            }

            // This is the case, when everything is the same.
            return true;
        }
    }
}