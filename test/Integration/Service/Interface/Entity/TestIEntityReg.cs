using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityReg
    {
        private IUnsafePlayerRepo _repo;
        private TestEntityReg _registration;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMUnsafePlayerRepo();
            this._registration = new TestEntityReg(this._repo);
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentNullException>(()=>new TestEntityReg(null));
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>this._registration.Register(null));

            var p = new UnsafePlayer(new Name("Hal"));
            Assert.IsFalse(p.IsIdSet());
            var l = this._registration.Register(p);
            Assert.IsTrue(p.IsIdSet());
            Assert.AreEqual(p.Id, l.Id);

            var a = new UnsafePlayer(new Name("Jack"));
            Assert.IsFalse(a.IsIdSet());
            var y = this._registration.Register(a);
            Assert.IsTrue(a.IsIdSet());
            Assert.AreEqual(a.Id, y.Id);

            var e = new UnsafePlayer(new Name("Sierra"));
            Assert.IsFalse(e.IsIdSet());
            var r = this._registration.Register(e);
            Assert.IsTrue(e.IsIdSet());
            Assert.AreEqual(e.Id, r.Id);
        }
    }

    public class TestEntityReg
        : IEntityReg<UnsafePlayer, Id, int>
    {
        public TestEntityReg(IUnsafePlayerRepo repo)
            : base(repo)
        { }
    }
}