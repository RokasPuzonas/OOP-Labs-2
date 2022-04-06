using System;
using System.Text;
using Bogus;
using LD_24.Code;

namespace LD_24Tests
{
    public class StringLinkedListTests : LinkedListTests<string>
    {
        private readonly Faker faker = new Faker("en");

        protected override string CreateItem()
        {
            return faker.Name.FullName();
        }
    }
}
