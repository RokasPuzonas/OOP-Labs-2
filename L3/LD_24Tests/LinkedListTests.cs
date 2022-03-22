using System;
using Xunit;
using FluentAssertions;
using LD_24.Code;
using System.Collections;

namespace LD_24Tests
{
    public abstract class LinkedListTests<T>
        where T : IComparable<T>, IEquatable<T>
    {
        protected abstract LinkedList<T> CreateNewList();
        protected abstract T CreateItem();

        [Fact]
        public void New_List_Is_Empty()
        {
            LinkedList<T> list = CreateNewList();

            list.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void Assert_List_Is_Not_Empty()
        {
            LinkedList<T> list = CreateNewList();

            list.Add(CreateItem());

            list.IsEmpty().Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void Retrieve_How_Many_Were_Added(int count)
        {
            LinkedList<T> list = CreateNewList();

            for (int i = 0; i < count; i++)
            {
                list.Add(CreateItem());
            }

            list.Count().Should().Be(count);
        }

        [Fact]
        public void IEnumerable_Is_Implemented_Correctly()
        {
            LinkedList<T> list = CreateNewList();

            var item1 = CreateItem();
            var item2 = CreateItem();
            var item3 = CreateItem();

            list.Add(item1);
            list.Add(item2);
            list.Add(item3);

            var newList = new System.Collections.Generic.List<T>();
            foreach (var item in list)
            {
                newList.Add(item);
            }

            newList[0].Should().Be(item1);
            newList[1].Should().Be(item2);
            newList[2].Should().Be(item3);
        }
    }
}
