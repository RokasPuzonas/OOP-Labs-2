using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Publication
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
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