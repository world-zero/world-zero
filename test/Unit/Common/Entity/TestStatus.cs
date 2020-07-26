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
            this._s.Id = this._name;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Status();
            Assert.IsNull(s.Id);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._name, this._s.Id);
            this._s.Id = "Test";
            Assert.AreEqual("Test", this._s.Id);
            Assert.Throws<ArgumentException>(()=>this._s.Id = null);
            Assert.Throws<ArgumentException>(()=>this._s.Id = "");
            Assert.Throws<ArgumentException>(()=>this._s.Id = "     ");
            Assert.AreEqual("Test", this._s.Id);
        }
    }
}