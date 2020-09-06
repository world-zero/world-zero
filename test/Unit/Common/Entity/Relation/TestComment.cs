using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TestComment
    {
        [Test]
        public void TestCommentValue()
        {
            var id0 = new Id(3);
            var id1 = new Id(5);
            var c = new Comment(id0, id1, "idk something");
            Assert.Throws<ArgumentException>(()=>new Comment(id0, id1, null));
            Assert.Throws<ArgumentException>(()=>new Comment(id0, id1, ""));
            Assert.Throws<ArgumentException>(()=>new Comment(id0, id1, "  "));
            Assert.Throws<ArgumentException>(()=>c.Value = null);
            Assert.Throws<ArgumentException>(()=>c.Value = "");
            Assert.Throws<ArgumentException>(()=>c.Value = "   ");
        }
    }
}