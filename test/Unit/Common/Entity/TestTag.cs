using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestTag
    {
        private Tag _t;
        private string _name;

        [SetUp]
        public void Setup()
        {
            this._name = "boop";

            this._t = new Tag();
            this._t.Name = this._name;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Tag();
            Assert.IsNull(s.Name);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._name, this._t.Name);
            this._t.Name = "Test";
            Assert.AreEqual("Test", this._t.Name);
            Assert.Throws<ArgumentException>(()=>this._t.Name = null);
            Assert.Throws<ArgumentException>(()=>this._t.Name = "");
            Assert.Throws<ArgumentException>(()=>this._t.Name = "     ");
            Assert.AreEqual("Test", this._t.Name);
        }
    }
}