using System;
using System.Text;
using LD_24.Code;
using Bogus;

namespace LD_24Tests
{
    public class IntLinkedListTests : LinkedListTests<int>
    {
        private readonly Faker faker = new Faker("en");

        protected override int CreateItem()
        {
            return faker.Random.Number(100);
        }
    }
}
