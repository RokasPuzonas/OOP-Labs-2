using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Order
    {
        public string CustomerSurname { get; set; }
        public string CustomerName { get; set; }
        public string ProductID { get; set; }
        public int ProductAmount { get; set; }

        public Order(string customerSurname, string customerName, string productID, int productAmount)
        {
            CustomerSurname = customerSurname;
            CustomerName = customerName;
            ProductID = productID;
            ProductAmount = productAmount;
        }

        public override string ToString()
        {
            return String.Format("Order{Name = '{0}'}", CustomerName);
        }

        public static bool operator <(Order a, Order b)
        {
            if (a.ProductAmount > b.ProductAmount)
            {
                return true;
            }
            else if (a.ProductAmount == b.ProductAmount)
            {
                int surnameCompare = a.CustomerSurname.CompareTo(b.CustomerSurname);
                if (surnameCompare < 0)
                {
                    return true;
                }
                else if (surnameCompare == 0)
                {
                    return a.CustomerName.CompareTo(b.CustomerName) < 0;
                }
            }

            return false;
        }
        public static bool operator >(Order a, Order b)
        {
            return !(a < b && a == b);
        }
        public static bool operator ==(Order a, Order b)
        {
            return a.ProductAmount == b.ProductAmount && a.CustomerName == b.CustomerName && a.CustomerSurname == b.CustomerSurname;
        }
        public static bool operator !=(Order a, Order b)
        {
            return !(a == b);
        }
    }
}