using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// Holds informations about a single product
    /// </summary>
    public class Product: IEquatable<Product>, IComparable<Product>
    {
        /// <summary>
        /// Identification number of product
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Name of product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price of product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="name">Name</param>
        /// <param name="price">Price</param>
        public Product(string iD, string name, decimal price)
        {
            ID = iD;
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Check if this has the same sorting order as othe product
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Product other)
        {
            return ID == other.ID &&
                   Name == other.Name &&
                   Price == other.Price;
        }

        public override int GetHashCode()
        {
            int hashCode = 560300832;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares 2 products by their IDs
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Product other)
        {
            return ID.CompareTo(other.ID);
        }

        public override string ToString()
        {
            return String.Format("Product{ID = '{0}'}", ID);
        }
    }
}
