using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using NUnit.Framework;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestIUnsafeTaggedEntity
    {
        private Id _id0;
        private Id _id1;
        private Name _name;
        private TestTaggedEntity _e;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(320);
            this._id1 = new Id(30);
            this._name = new Name("#Foo");
            this._e = new TestTaggedEntity(this._id0, this._id1, this._name);
        }

        [Test]
        public void TestTagId()
        {
            Assert.AreEqual(this._name, this._e.RightId);
            Assert.AreEqual(this._name, this._e.TagId);
            var newName = new Name("#new");
            this._e.TagId = newName;
            Assert.AreEqual(newName, this._e.TagId);
            Assert.AreEqual(this._e.RightId, this._e.TagId);
        }
    }

    public class TestTaggedEntity : IUnsafeTaggedEntity<Id, int>
    {
        public TestTaggedEntity(Id leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public TestTaggedEntity(Id id, Id leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new TestTaggedEntity(this.Id, this.LeftId, this.RightId);
        }
    }
}