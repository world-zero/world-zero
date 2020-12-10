using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestIUnsafeProxy
    {
        private UnsafePlayer _unsafe;
        private TestUnsafeProxy _proxy;

        [SetUp]
        public void Setup()
        {
            this._unsafe = new UnsafePlayer(new Name("x"));
            this._proxy = new TestUnsafeProxy(this._unsafe);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new TestUnsafeProxy(null));

            Id oldId = this._unsafe.Id;
            this._unsafe.Id = new Id(234);
            Assert.AreEqual(oldId, this._proxy.Id);
        }

        [Test]
        public void TestIsIdSet()
        {
            Assert.IsFalse(this._unsafe.IsIdSet());
            Assert.IsFalse(this._proxy.IsIdSet());
            this._unsafe.Id = new Id(2);
            Assert.IsTrue(this._unsafe.IsIdSet());
            Assert.IsFalse(this._proxy.IsIdSet());
        }

        [Test]
        public void TestCloneUnsafeEntity()
        {
            UnsafePlayer p = this._proxy.CloneUnsafeEntity();
            var oldId = this._proxy.Id;
            var id = new Id(324);
            p.Id = id;
            Assert.AreNotEqual(id, this._proxy.Id);
            Assert.AreEqual(oldId, this._proxy.Id);
        }
    }

    public class TestUnsafeProxy : IUnsafeProxy<UnsafePlayer, Id, int>
    {
        public TestUnsafeProxy(UnsafePlayer p)
            : base(p)
        { }

        public override IEntity<Id, int> Clone()
        {
            return null;
        }
    }
}