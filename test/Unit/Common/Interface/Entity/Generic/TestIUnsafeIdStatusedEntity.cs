using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestIUnsafeIdStatusedEntity
    {
        [Test]
        public void TestStatusId()
        {
            var statusId = new Name("idk");
            var e = new TestIdStatusedEntity(statusId);
            Assert.AreEqual(statusId, e.StatusId);
            statusId = new Name("something");
            e.StatusId = statusId;
            Assert.AreEqual(statusId, e.StatusId);
            Assert.Throws<ArgumentNullException>(()=>e.StatusId = null);
            Assert.AreEqual(statusId, e.StatusId);
        }
    }

    public class TestIdStatusedEntity : IUnsafeIdStatusedEntity
    {
        public TestIdStatusedEntity(Id id, Name statusId)
            : base(id, statusId)
        { }

        public TestIdStatusedEntity(Name statusId)
            : base(statusId)
        { }

        public override IEntity<Id, int> Clone()
        {
            return null;
        }
    }
}