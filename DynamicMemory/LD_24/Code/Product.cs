using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string iD, string name, decimal price)
        {
            ID = iD;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return String.Format("Product{ID = '{0}'}", ID);
        }
    }
}
