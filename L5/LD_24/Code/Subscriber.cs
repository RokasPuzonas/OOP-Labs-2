using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_24.Code
{
    public class Subscriber
    {
        public DateTime EnterDate { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public int SubscriptionStart { get; set; }
        public int SubscriptionLength { get; set; }
        public string SubscriptionID { get; set; }
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