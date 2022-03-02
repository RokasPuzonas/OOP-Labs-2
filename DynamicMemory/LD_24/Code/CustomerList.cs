using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class CustomerList : IEnumerable<Customer>
    {
        private CustomerNode head;
        private CustomerNode tail;

        public void AddToEnd(Customer customer)
        {
            CustomerNode node = new CustomerNode(customer);
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

        public void AddToStart(Customer customer)
        {
            CustomerNode node = new CustomerNode(customer);
            if (tail != null && head != null)
            {
                node.Next = head;
                head = node;
            }
            else
            {
                tail = node;
                head = node;
            }
        }

        public Customer Get(int index)
        {
            int i = 0;
            CustomerNode current = head;
            while (i < index && current != null)
            {
                current = head.Next;
                i++;
            }
            return current.Data;
        }

        public int Count()
        {
            int count = 0;
            CustomerNode current = head;
            while (current != null)
            {
                current = current.Next;
                count++;
            }
            return count;
        }

        public void Sort()
        {
            for (CustomerNode nodeA = head; nodeA != null; nodeA = nodeA.Next)
            {
                CustomerNode min = nodeA;
                for (CustomerNode nodeB = nodeA.Next; nodeB != null; nodeB = nodeB.Next)
                {
                    if (nodeB.Data < min.Data)
                    {
                        min = nodeB;
                    }
                }

                Customer tmp = nodeA.Data;
                nodeA.Data = min.Data;
                min.Data = tmp;
            }
        }

        public override string ToString()
        {
            return String.Format("CustomerList{ Count = '{0}' }", Count());
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            CustomerNode current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}