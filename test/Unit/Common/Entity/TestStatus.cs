using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestStatus
    {
        private Status _s;
        private string _name;

        [SetUp]
        public void Setup()
        {
            this._name = "boop";

            this._s = new Status();
            this._s.Name = this._name;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Status();
            Assert.IsNull(s.Name);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._name, this._s.Name);
            this._s.Name = "Test";
            Assert.AreEqual("Test", this._s.Name);
            Assert.Throws<ArgumentException>(()=>this._s.Name = null);
            Assert.Throws<ArgumentException>(()=>this._s.Name = "");
            Assert.Throws<ArgumentException>(()=>this._s.Name = "     ");
            Assert.AreEqual("Test", this._s.Name);
        }
    }
}