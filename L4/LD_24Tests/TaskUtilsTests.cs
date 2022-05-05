using System;
using Xunit;
using LD_24.Code;
using System.Collections.Generic;
using FluentAssertions;

namespace LD_24Tests
{
    public class TaskUtilsTests
    {
        [Fact]
        public void TestFindAllClasses()
        {
            List<Actor> actors = new List<Actor>();
            actors.Add(new NPC("foo", "bar", "baz", "ClassAAA", 10, 10, 10, 10, "biz"));
            actors.Add(new Hero("foo", "bar", "baz", "ClassBBB", 10, 10, 10, 10, 10, 10, 10, 10));
            actors.Add(new NPC("foo", "bar", "baz", "ClassBBB", 10, 10, 10, 10, "baz"));
            actors.Add(new Hero("foo", "bar", "baz", "ClassCCC", 10, 10, 10, 10, 10, 10, 10, 10));
            List<string> classes = TaskUtils.FindAllClasses(actors);
            classes.Should().BeEquivalentTo("ClassAAA", "ClassBBB", "ClassCCC");
        }

        [Fact]
        public void TestFilterNPCsByAttack()
        {
            List<Actor> actors = new List<Actor>();
            var actor1 = new NPC("foo", "bar", "baz", "ClassAAA", 10, 10, 9, 10, "biz");
            var actor2 = new Hero("foo", "bar", "baz", "ClassBBB", 10, 10, 10, 10, 10, 10, 10, 10);
            var actor3 = new NPC("foo", "bar", "baz", "ClassBBB", 10, 10, 10, 10, "baz");
            var actor4 = new Hero("foo", "bar", "baz", "ClassCCC", 10, 10, 10, 10, 10, 10, 10, 10);
            var actor5 = new NPC("foo", "bar", "baz", "ClassBBB", 10, 10, 5, 10, "baz");

            actors.Add(actor1);
            actors.Add(actor2);
            actors.Add(actor3);
            actors.Add(actor4);
            actors.Add(actor5);
            var NPCs = TaskUtils.FilterNPCsByAttack(actors, 10);
            NPCs.Should().BeEquivalentTo(new List<NPC> { actor1, actor5 });
        }
    }
}
