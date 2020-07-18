using System;
using WorldZero.Common.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestStatus
    {
        private Status _s;
        private string _statusName;

        [SetUp]
        public void Setup()
        {
            this._statusName = "boop";

            this._s = new Status();
            this._s.StatusName = this._statusName;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Status();
            Assert.IsNull(s.StatusName);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestStatusName()
        {
            Assert.AreEqual(this._statusName, this._s.StatusName);
            this._s.StatusName = "Test";
            Assert.AreEqual("Test", this._s.StatusName);
            Assert.Throws<ArgumentException>(()=>this._s.StatusName = null);
            Assert.Throws<ArgumentException>(()=>this._s.StatusName = "");
            Assert.Throws<ArgumentException>(()=>this._s.StatusName = "     ");
            Assert.AreEqual("Test", this._s.StatusName);
        }
    }
}