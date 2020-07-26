using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestEra
    {
        private Era _e;
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;

        [SetUp]
        public void Setup()
        {
            this._name = "Testing";
            this._startDate = DateTime.UtcNow;
            this._endDate = DateTime.UtcNow;

            this._e = new Era();
            this._e.Id = this._name;
            this._e.StartDate = this._startDate;
            this._e.EndDate = this._endDate;
        }

        [Test]
        public void TestDefaultValues()
        {
            var e = new Era();
            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(e.StartDate.ToString("MM:dd:yyyy HH"), DateTime.UtcNow.ToString("MM:dd:yyyy HH"));
            Assert.IsNull(e.Id);
            Assert.IsNull(e.EndDate);
        }

        [Test]
        public void TEstId()
        {
            Assert.AreEqual(this._name, this._e.Id);
            this._e.Id = "Test";
            Assert.AreEqual("Test", this._e.Id);
            Assert.Throws<ArgumentException>(()=>this._e.Id = null);
            Assert.Throws<ArgumentException>(()=>this._e.Id = "");
            Assert.Throws<ArgumentException>(()=>this._e.Id = "     ");
            Assert.AreEqual("Test", this._e.Id);
        }

        [Test]
        public void TestStartDate()
        {
            Assert.AreEqual(this._e.StartDate, this._startDate);
            Assert.Throws<ArgumentException>(()=>this._e.StartDate = new DateTime(3000, 5, 1, 8, 30, 52));
            Assert.AreEqual(this._e.StartDate, this._startDate);
        }

        [Test]
        public void TestEndDate()
        {
            Assert.AreEqual(this._e.EndDate, this._endDate);
            Assert.Throws<ArgumentException>(()=>this._e.EndDate = new DateTime(3000, 5, 1, 8, 30, 52));
            Assert.AreEqual(this._e.EndDate, this._endDate);
            this._e.EndDate = null;
            Assert.IsNull(this._e.EndDate);
        }

        [Test]
        public void TestDates()
        {
            Assert.AreEqual(this._e.StartDate, this._startDate);
            Assert.AreEqual(this._e.EndDate, this._endDate);
            Assert.Throws<ArgumentException>(()=>this._e.EndDate = new DateTime(1, 1, 1));
            Assert.Throws<ArgumentException>(()=>this._e.StartDate = new DateTime(3000, 1, 1));
        }
    }
}