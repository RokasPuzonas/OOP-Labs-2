using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Class used for storing a single order
    /// </summary>
    public class Order: IEquatable<Order>, IComparable<Order>
    {
        /// <summary>
        /// Surname of customer who ordered
        /// </summary>
        public string CustomerSurname { get; set; }
        /// <summary>
        /// Name of customer who ordered
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// ID of ordered product
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// Amount of ordered products
        /// </summary>
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

        public int CompareTo(Order other)
        {
            if (ProductAmount > other.ProductAmount)
            {
                return 1;
            }
            else if (ProductAmount == other.ProductAmount)
            {
                int surnameCompare = CustomerSurname.CompareTo(other.CustomerSurname);
                if (surnameCompare < 0)
                {
                    return 1;
                }
                else if (surnameCompare == 0 && CustomerName.CompareTo(other.CustomerName) < 0)
                {
                    return 1;
                }
            }

            return Equals(other) ? 0 : -1;
        }

        public bool Equals(Order other)
        {
            return CustomerSurname == other.CustomerSurname &&
                   CustomerName == other.CustomerName &&
                   ProductID == other.ProductID &&
                   ProductAmount == other.ProductAmount;
        }

        public override int GetHashCode()
        {
            int hashCode = -273364163;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerSurname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductID);
            hashCode = hashCode * -1521134295 + ProductAmount.GetHashCode();
            return hashCode;
        }
    }
}