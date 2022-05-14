using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    /// <summary>
    /// A single subscriber
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// When subscriber input their data
        /// </summary>
        public DateTime EnterDate { get; set; }

        /// <summary>
        /// Subscribers surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Subscribers address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Starting month of subscription
        /// </summary>
        public int SubscriptionStart { get; set; }

        /// <summary>
        /// Length in months of subcription
        /// </summary>
        public int SubscriptionLength { get; set; }

        /// <summary>
        /// ID of subcription that is subscribed to
        /// </summary>
        public string SubscriptionID { get; set; }

        /// <summary>
        /// Number of copies of subscription
        /// </summary>
        public int SubscrionCount { get; set; }

        public Subscriber(DateTime enterDate, string surname, string address, int subscriptionStart, int subscriptionLength, string subscriptionID, int subscrionCount)
        {
            EnterDate = enterDate;
            Surname = surname;
            Address = address;
            SubscriptionStart = subscriptionStart;
            SubscriptionLength = subscriptionLength;
            SubscriptionID = subscriptionID;
            SubscrionCount = subscrionCount;
        }
    }
}