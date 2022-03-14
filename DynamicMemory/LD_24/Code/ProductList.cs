using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class ProductList : IEnumerable<Product>
    {
        class ProductNode
        {
            public Product Data { get; set; }
            public ProductNode Next { get; set; }

            public ProductNode(Product data = null, ProductNode next = null)
            {
                Data = data;
                Next = next;
            }
        }

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