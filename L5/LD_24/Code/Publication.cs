using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// A publication
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// ID of publication
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Title of publication
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Publisher of publication
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Price of publication per month
        /// </summary>
        public decimal PricePerMonth { get; set; }

        public Publication(string iD, string title, string publisher, decimal pricePerMonth)
        {
            ID = iD;
            Title = title;
            Publisher = publisher;
            PricePerMonth = pricePerMonth;
        }
    }
}