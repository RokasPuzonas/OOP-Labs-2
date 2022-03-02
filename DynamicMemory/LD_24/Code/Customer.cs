using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Customer
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }
        public int ProductAmount { get; set; }

        public Customer(string surname, string name, string productID, int productAmount)
        {
            Surname = surname;
            Name = name;
            ProductID = productID;
            ProductAmount = productAmount;
        }

        public override string ToString()
        {
            return String.Format("Customer{Name = '{0}'}", Name);
        }

        public static bool operator <(Customer a, Customer b)
        {
            if (a.ProductAmount > b.ProductAmount)
            {
                return true;
            }
            else if (a.ProductAmount == b.ProductAmount)
            {
                int surnameCompare = a.Surname.CompareTo(b.Surname);
                if (surnameCompare < 0)
                {
                    return true;
                }
                else if (surnameCompare == 0)
                {
                    return a.Name.CompareTo(b.Name) < 0;
                }
            }

            return false;
        }
        public static bool operator >(Customer a, Customer b)
        {
            return !(a < b && a == b);
        }
        public static bool operator ==(Customer a, Customer b)
        {
            return a.ProductAmount == b.ProductAmount && a.Name == b.Name && a.Surname == b.Surname;
        }
        public static bool operator !=(Customer a, Customer b)
        {
            return !(a == b);
        }
    }
}