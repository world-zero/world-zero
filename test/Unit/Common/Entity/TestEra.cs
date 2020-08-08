using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestEra
    {
        private Era _e;
        private Name _name;
        private PastDate _startDate;
        private PastDate _endDate;

        [SetUp]
        public void Setup()
        {
            this._name = new Name("Testing");
            this._startDate = new PastDate(DateTime.UtcNow);
            this._endDate = new PastDate(DateTime.UtcNow);

            this._e = new Era(this._name, this._startDate, this._endDate);
        }

        [Test]
        public void TestDefaultCustomValues()
        {
            var name = new Name("Pumpkin Pie");
            var startDate = new PastDate(DateTime.UtcNow);
            var e = new Era(name, startDate);

            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                e.StartDate.Get.ToString("MM:dd:yyyy HH")
            );
            Assert.IsNull(e.EndDate);

            var endDate = new PastDate(DateTime.UtcNow);
            e = new Era(name, startDate, endDate);
            Assert.IsNotNull(e.EndDate);
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                e.EndDate.Get.ToString("MM:dd:yyyy HH")
            );
        }

        [Test]
        public void TestStartDate()
        {
            Assert.AreEqual(this._startDate, this._e.StartDate);
            Assert.Throws<ArgumentException>(()=>this._e.StartDate = new PastDate(new DateTime(3000, 5, 1, 8, 30, 52)));
            Assert.AreEqual(this._startDate, this._e.StartDate);
        }

        [Test]
        public void TestEndDate()
        {
            Assert.AreEqual(this._endDate, this._e.EndDate);
            Assert.Throws<ArgumentException>(()=>this._e.EndDate = new PastDate(new DateTime(3000, 5, 1, 8, 30, 52)));
            Assert.AreEqual(this._endDate, this._e.EndDate);
            this._e.EndDate = null;
            Assert.IsNull(this._e.EndDate);
        }

        [Test]
        public void TestDates()
        {
            Assert.AreEqual(this._startDate, this._e.StartDate);
            Assert.AreEqual(this._endDate, this._e.EndDate);
            Assert.Throws<ArgumentException>(()=>this._e.EndDate = new PastDate(new DateTime(1, 1, 1)));
            Assert.Throws<ArgumentException>(()=>this._e.StartDate = new PastDate(new DateTime(3000, 1, 1)));
        }
    }
}