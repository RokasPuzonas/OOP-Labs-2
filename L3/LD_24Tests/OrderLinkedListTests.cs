using System;
using System.Text;
using Bogus;
using LD_24.Code;

namespace LD_24Tests
{
    public class OrderLinkedListTests : LinkedListTests<Order>
    {
        private readonly Faker faker = new Faker("en");

        protected override Order CreateItem()
        {
            string customerSurname = faker.Name.FindName();
            string customerName = faker.Name.LastName();
            string productID = faker.Random.AlphaNumeric(5);
            int productAmount = faker.Random.Number(1, 100);
            return new Order(customerSurname, customerName, productID, productAmount);
        }
    }
}
