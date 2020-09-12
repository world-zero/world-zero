using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TestComment
    {
        private Id _id0;
        private Id _id1;
        private Comment _c;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._c = new Comment(this._id0, this._id1, "idk something");
        }

        [Test]
        public void TestCommentValue()
        {
            Assert.Throws<ArgumentException>(()=>
                new Comment(this._id0, this._id1, null));
            Assert.Throws<ArgumentException>(()=>
                new Comment(this._id0, this._id1, ""));
            Assert.Throws<ArgumentException>(()=>
                new Comment(this._id0, this._id1, "  "));
            Assert.Throws<ArgumentException>(()=>this._c.Value = null);
            Assert.Throws<ArgumentException>(()=>this._c.Value = "");
            Assert.Throws<ArgumentException>(()=>this._c.Value = "   ");
        }

        [Test]
        public void TestCount()
        {
            new Comment(this._id0, this._id1, "asdf", 1);
            Assert.Throws<ArgumentException>(()=>
                new Comment(this._id0, this._id1, "asdf", 0));
        }
    }
}