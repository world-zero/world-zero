using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using NUnit.Framework;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestABCFlaggedEntity
    {
        private Id _id0;
        private Id _id1;
        private Name _name;
        private TestFlaggedEntity _e;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(320);
            this._id1 = new Id(30);
            this._name = new Name("#Foo");
            this._e = new TestFlaggedEntity(this._id0, this._id1, this._name);
        }

        [Test]
        public void TestFlagId()
        {
            Assert.AreEqual(this._name, this._e.RightId);
            Assert.AreEqual(this._name, this._e.FlagId);
            var newName = new Name("#new");
            this._e.FlagId = newName;
            Assert.AreEqual(newName, this._e.FlagId);
            Assert.AreEqual(this._e.RightId, this._e.FlagId);
        }
    }

    public class TestFlaggedEntity : ABCFlaggedEntity<Id, int>
    {
        public TestFlaggedEntity(Id leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public TestFlaggedEntity(Id id, Id leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

    }
}