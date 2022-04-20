using System;
using Xunit;
using FluentAssertions;
using LD_24.Code;
using System.Collections;
using System.Linq;

namespace LD_24Tests
{
    public abstract class LinkedListTests<T>
        where T : IComparable<T>, IEquatable<T>
    {
        protected abstract T CreateItem();

        private LinkedList<T> CreateLoadedList(int count)
        {
            var list = new LinkedList<T>();

            for (int i = 0; i < count; i++)
            {
                list.Add(CreateItem());
            }

            return list;
        }

        [Fact]
        [Trait("TestedMethod", "IsEmpty")]
        public void New_List_Is_Empty()
        {
            LinkedList<T> list = new LinkedList<T>();

            list.IsEmpty().Should().BeTrue();
        }

        [Fact]
        [Trait("TestedMethod", "IsEmpty")]
        public void Assert_List_Is_Not_Empty()
        {
            LinkedList<T> list = new LinkedList<T>();
            list.Add(CreateItem());

            list.IsEmpty().Should().BeFalse();
        }

        [Theory]
        [Trait("TestedMethod", "Count")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void Retrieve_How_Many_Were_Added(int count)
        {
            LinkedList<T> list = CreateLoadedList(count);

            list.Count().Should().Be(count);
        }

        [Theory]
        [Trait("TestedMethod", "GetLast")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void Assert_Last_Element_Value(int count)
        {
            var list = CreateLoadedList(count);
            var last = CreateItem();
            list.Add(last);

            list.GetLast().Should().Be(last);
        }

        [Theory]
        [Trait("TestedMethod", "GetFirst")]
        [InlineData(1)]
        [InlineData(5)]
        public void Assert_First_Element_Value(int count)
        {
            var list = new LinkedList<T>();
            var first = CreateItem();
            list.Add(first);

            for (int i = 0; i < count; i++)
                list.Add(CreateItem());

            list.GetFirst().Should().Be(first);
        }

        [Theory]
        [Trait("TestedMethod", "RemoveLast")]
        [InlineData(1)]
        [InlineData(5)]
        public void RemoveLast_Returns_Removed_Value(int count)
        {
            var list = CreateLoadedList(count);
            var last = list.GetLast();

            list.RemoveLast().Should().Be(last);
        }

        [Theory]
        [Trait("TestedMethod", "IEnumerable")]
        [InlineData(1)]
        [InlineData(5)]
        public void IEnumerable_Is_Implemented_Correctly(int count)
        {
            var genericList = new System.Collections.Generic.List<T>();
            LinkedList<T> list = new LinkedList<T>();

            for (int i = 0; i < count; i++)
            {
                var item = CreateItem();
                list.Add(item);
                genericList.Add(item);
            }

            // Checks the enumerators "list" and "genericList" contain the same values
            genericList.Zip(list, (a, b) => a.Equals(b)).All((t) => t).Should();
        }

        [Fact]
        [Trait("TestedMethod", "ToString")]
        public void Assert_ToString_Is_Overridden()
        {
            var list = new LinkedList<T>();
            Assert.True(list.ToString() != list.GetType().ToString());
        }

        [Fact]
        [Trait("TestedMethod", "Sort")]
        public void Sort_Does_Nothing_On_Empty_List()
        {
            var list = new LinkedList<T>();
            var listCopy = new LinkedList<T>(list);
            list.Sort();

            list.Equals(listCopy).Should().BeTrue();
        }

        [Theory]
        [Trait("TestedMethod", "Sort")]
        [InlineData(1)]
        [InlineData(5)]
        public void Items_In_Correct_Order_After_Sort(int count)
        {
            var list = CreateLoadedList(count);
            list.Sort();

            var itemPairs = list.Zip(list.Skip(1), (a, b) => Tuple.Create(a, b));
            foreach (var tuple in itemPairs)
            {
                Assert.True(tuple.Item1.CompareTo(tuple.Item2) >= 0);
            }
        }

        [Theory]
        [Trait("TestedMethod", "Sort")]
        [InlineData(1)]
        [InlineData(5)]
        public void Does_Nothing_If_Already_Sorted(int count)
        {
            var list = CreateLoadedList(count);
            list.Sort();

            var listCopy = new LinkedList<T>(list);
            list.Sort();

            list.Equals(listCopy).Should().BeTrue();
        }
    }
}
