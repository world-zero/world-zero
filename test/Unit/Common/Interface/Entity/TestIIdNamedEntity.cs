using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity
{
    [TestFixture]
    public class TestIIdNamedEntity
    {
        private string _sName;
        private TestIdNamedEntity _e;

        [SetUp]
        public void Setup()
        {
            this._sName = "Pizza";
            this._e = new TestIdNamedEntity(this._sName);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(new Name(this._sName), this._e.Name);
            Assert.Throws<ArgumentException>(()=>new TestIdNamedEntity(null));
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(new Name(this._sName), this._e.Name);
            this._e.Name = new Name("Steve");
            Assert.AreEqual(new Name("Steve"), this._e.Name);
            Assert.Throws<ArgumentNullException>(()=>this._e.Name = null);
            Assert.AreEqual(new Name("Steve"), this._e.Name);
        }
    }

    public class TestIdNamedEntity : IIdNamedEntity
    {
        public TestIdNamedEntity(string name)
            : base(new Name(name))
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return null;
        }
    }
}