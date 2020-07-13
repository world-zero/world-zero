using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestEra
    {
        private Era _e;
        private string _eraName;
        private DateTime _startDate;
        private DateTime _endDate;

        [SetUp]
        public void Setup()
        {
            this._eraName = "Testing";
            this._startDate = DateTime.UtcNow;
            this._endDate = DateTime.UtcNow;

            this._e = new Era();
            this._e.EraName = this._eraName;
            this._e.StartDate = this._startDate;
            this._e.EndDate = this._endDate;
        }

        [Test]
        public void TestDefaultValues()
        {
            var e = new Era();
            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(e.StartDate.ToString("MM:dd:yyyy HH"), DateTime.UtcNow.ToString("MM:dd:yyyy HH"));
            Assert.IsNull(e.EraName);
            Assert.IsNull(e.EndDate);
        }

        [Test]
        public void TEstEraName()
        {
            Assert.AreEqual(this._eraName, this._e.EraName);
            this._e.EraName = "Test";
            Assert.AreEqual("Test", this._e.EraName);
            Assert.Throws<ArgumentException>(()=>this._e.EraName = null);
            Assert.Throws<ArgumentException>(()=>this._e.EraName = "");
            Assert.Throws<ArgumentException>(()=>this._e.EraName = "     ");
            Assert.AreEqual("Test", this._e.EraName);
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