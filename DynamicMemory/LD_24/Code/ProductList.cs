using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class ProductList : IEnumerable<Product>
    {
        private ProductNode head;
        private ProductNode tail;

        public void AddToEnd(Product product)
        {
            ProductNode node = new ProductNode(product);
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

        public void AddToStart(Product product)
        {
            ProductNode node = new ProductNode(product);
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

        public Product Get(int index)
        {
            int i = 0;
            ProductNode current = head;
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
            ProductNode current = head;
            while (current != null)
            {
                current = current.Next;
                count++;
            }
            return count;
        }

        public override string ToString()
        {
            return String.Format("ProductList{ Count = '{0}' }", Count());
        }

        public IEnumerator<Product> GetEnumerator()
        {
            ProductNode current = head;
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