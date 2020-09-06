using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.Entity.RAM;
using WorldZero.Service.Interface;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface
{
    [TestFixture]
    public class TestIEntityRegistration
    {
        private IPlayerRepo _repo;
        private TestEntityRegistration _registration;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMPlayerRepo();
            this._registration = new TestEntityRegistration(this._repo);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentNullException>(()=>new TestEntityRegistration(null));
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>this._registration.Register(null));

            var p = new Player(new Name("Hal"));
            Assert.IsFalse(p.IsIdSet());
            var l = this._registration.Register(p);
            Assert.IsTrue(p.IsIdSet());
            Assert.AreEqual(p.Id, l.Id);

            var a = new Player(new Name("Jack"));
            Assert.IsFalse(a.IsIdSet());
            var y = this._registration.Register(a);
            Assert.IsTrue(a.IsIdSet());
            Assert.AreEqual(a.Id, y.Id);

            var e = new Player(new Name("Sierra"));
            Assert.IsFalse(e.IsIdSet());
            var r = this._registration.Register(e);
            Assert.IsTrue(e.IsIdSet());
            Assert.AreEqual(e.Id, r.Id);
        }
    }

    public class TestEntityRegistration
        : IEntityRegistration<Player, Id, int>
    {
        public TestEntityRegistration(IPlayerRepo repo)
            : base(repo)
        { }
    }
}