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
            this._t.Id = this._name;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Tag();
            Assert.IsNull(s.Id);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._name, this._t.Id);
            this._t.Id = "Test";
            Assert.AreEqual("Test", this._t.Id);
            Assert.Throws<ArgumentException>(()=>this._t.Id = null);
            Assert.Throws<ArgumentException>(()=>this._t.Id = "");
            Assert.Throws<ArgumentException>(()=>this._t.Id = "     ");
            Assert.AreEqual("Test", this._t.Id);
        }
    }
}