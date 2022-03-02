using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class CustomerNode
    {
        public Customer Data { get; set; }
        public CustomerNode Next { get; set; }

        public CustomerNode(Customer data = null, CustomerNode next = null)
        {
            Data = data;
            Next = next;
        }
    }
}