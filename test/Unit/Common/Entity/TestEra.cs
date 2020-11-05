using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
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

            this._e = new Era(
                this._name,
                this._startDate,
                null,
                10,
                20,
                30,
                1,
                true,
                this._endDate
            );
        }

        [Test]
        public void TestDefaultValues()
        {
            var name = new Name("Pumpkin Pie");
            var startDate = new PastDate(DateTime.UtcNow);
            var e = new Era(name, startDate);

            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                e.StartDate.Get.ToString("MM:dd:yyyy HH")
            );
            Assert.AreEqual(new Level(2), e.TaskLevelDelta);
            Assert.AreEqual(20, e.MaxPraxises);
            Assert.AreEqual(1, e.MaxTasks);
            Assert.AreEqual(2, e.MaxTasksReiterator);
            Assert.IsNull(e.EndDate);
        }

        [Test]
        public void TestCustomValues()
        {
            var name = new Name("Pumpkin Pie");
            var startDate = new PastDate(DateTime.UtcNow);
            var endDate = new PastDate(DateTime.UtcNow);
            var e = new Era(
                name,
                startDate,
                new Level(5),
                100,
                2,
                9,
                1,
                true,
                endDate
            );
            Assert.IsNotNull(e.EndDate);
            Assert.AreEqual(new Level(5), e.TaskLevelDelta);
            Assert.AreEqual(100, e.MaxPraxises);
            Assert.AreEqual(2, e.MaxTasks);
            Assert.AreEqual(9, e.MaxTasksReiterator);
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

        [Test]
        public void TestMaxPraxises()
        {
            this._e.MaxPraxises = 2;
            this._e.MaxPraxises = 1;
            Assert.Throws<ArgumentException>(()=>this._e.MaxPraxises = 0);
        }

        [Test]
        public void TestMaxTasks()
        {
            this._e.MaxTasks = 2;
            this._e.MaxTasksReiterator = 2;
            this._e.MaxTasks = 1;
            Assert.Throws<ArgumentException>(()=>this._e.MaxTasks = 0);
            Assert.Throws<ArgumentException>(()=>this._e.MaxTasks = 1000);
        }

        [Test]
        public void TestMaxTasksReiterator()
        {
            this._e.MaxTasks = 1;
            this._e.MaxTasksReiterator = 2;
            this._e.MaxTasksReiterator = 1;
            Assert.Throws<ArgumentException>(()=>
                this._e.MaxTasksReiterator = 0);
            this._e.MaxTasksReiterator = 100;
            this._e.MaxTasks = 100;
            Assert.Throws<ArgumentException>(()=>
                this._e.MaxTasksReiterator = 10);
        }

        [Test]
        public void TestPenaltyDeduction()
        {
            this._e.PenaltyDeduction = 0;
            Assert.Throws<ArgumentException>(()=>
                this._e.PenaltyDeduction = -1);
        }

        [Test]
        public void TestApplyPenaltyFlatHappy()
        {
            this._e.IsFlatPenalty = true;
            this._e.PenaltyDeduction = 10;
            Assert.AreEqual(
                new PointTotal(90),
                this._e.ApplyPenalty(new PointTotal(100))
            );
        }

        [Test]
        public void TestApplyPenaltyFlatSad()
        {
            this._e.IsFlatPenalty = true;
            Assert.Throws<ArgumentNullException>(()=>
                this._e.ApplyPenalty(null));

            this._e.PenaltyDeduction = 1000;
            Assert.AreEqual(
                new PointTotal(0),
                this._e.ApplyPenalty(new PointTotal(10)));

            this._e.PenaltyDeduction = Convert.ToDouble(int.MaxValue);
            this._e.PenaltyDeduction += 10000;
            Assert.Throws<ArgumentException>(()=>
                this._e.ApplyPenalty(new PointTotal(324)));
        }

        [Test]
        public void TestApplyPenaltyPercentHappy()
        {
            this._e.IsFlatPenalty = false;
            this._e.PenaltyDeduction = 0.10;
            Assert.AreEqual(
                new PointTotal(90),
                this._e.ApplyPenalty(new PointTotal(100))
            );
        }

        [Test]
        public void TestApplyPenaltyPercentSad()
        {
            this._e.IsFlatPenalty = false;
            this._e.PenaltyDeduction = 1.1;
            Assert.AreEqual(
                new PointTotal(0),
                this._e.ApplyPenalty(new PointTotal(10)));
        }
    }
}