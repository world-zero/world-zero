using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestIEntity
    {
        [Test]
        public void TestId()
        {
            var id = new Id(3);
            var e = new TestEntity();
            Assert.AreEqual(new Id(0), e.Id, "The default Id did not get set correctly.");

            Assert.IsFalse(e.IsIdSet(), "IEntity.IsIdSet() is reporting a false positive.");

            Assert.Throws<ArgumentNullException>(()=>e.Id = null, "An exception was not thrown while attempting to set the Id to NULL.");
            Assert.IsNotNull(e.Id, "The Id was set to NULL when it should not have been.");

            Assert.IsFalse(e.IsIdSet(), "IEntity.IsIdSet() is reporting a false positive.");
            Assert.AreEqual(new Id(0), e.Id, "The default Id did not get maintained correctly.");

            e.Id = id;
            Assert.AreEqual(id, e.Id, "The Id did not get adjusted correctly.");

            Assert.IsTrue(e.IsIdSet(), "IEntity.IsIdSet() is reporting a false negative.");

            Assert.Throws<ArgumentException>(()=>e.Id = new Id(1999));
            Assert.AreEqual(id, e.Id, "The Id get adjusted when it should not have been.");

            Assert.Throws<ArgumentNullException>(()=>e.Id = null);
            Assert.IsNotNull(e.Id, "The Id was set to NULL when it should not have been.");
        }

        [Test]
        public void TestGetUniqueRules()
        {
            var e = new TestEntity();
            Assert.IsNotNull(e.GetUniqueRules());
        }
    }

    public class TestEntity : IEntity<Id, int>
    {
        public TestEntity()
            : base(new Id(0))
        { }

        public override IEntity<Id, int> Clone()
        {
            return null;
        }
    }
}