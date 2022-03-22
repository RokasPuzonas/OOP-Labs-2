using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class LinkedList<T> : IEnumerable<T>
        where T: IComparable<T>, IEquatable<T>
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

        public void Add(T customer)
        {
            Node node = new Node(customer);
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

        public int Count()
        {
            int count = 0;
            for (Node d = head; d != null; d = d.Next)
            {
                count++;
            }
            return count;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

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

        public override string ToString()
        {
            return String.Format("LinkedList<{0}>{ Count = '{0}' }", Count());
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node d = head; d != null; d = d.Next)
                yield return d.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}