using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface
{
    [TestFixture]
    public class TestIIdNamedEntity
    {
        private TestIdNamedEntity _e;
        private int _id;
        private Name _name;

        [SetUp]
        public void CreateInstance()
        {
            this._id = 1;
            this._name = new Name("Pizza");
            this._e = new TestIdNamedEntity()
            {
                Id = this._id,
                Name = this._name.Get
            };
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._name.Get, this._e.Name);
            this._e.Name = "Test";
            Assert.AreEqual("Test", this._e.Name);
            Assert.Throws<ArgumentException>(()=>this._e.Name = null);
            Assert.Throws<ArgumentException>(()=>this._e.Name = "");
            Assert.Throws<ArgumentException>(()=>this._e.Name = "     ");
            Assert.AreEqual("Test", this._e.Name);
        }
    }

    public class TestIdNamedEntity : IIdNamedEntity { }
}