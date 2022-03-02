using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class ProductNode
    {
        public Product Data { get; set; }
        public ProductNode Next { get; set; }

        public ProductNode(Product data = null, ProductNode next = null)
        {
            Data = data;
            Next = next;
        }
    }
}