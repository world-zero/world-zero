using System;
using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestEra
    {
        private UnsafeEra _e;
        private Name _name;
        private PastDate _startDate;
        private PastDate _endDate;

        [SetUp]
        public void Setup()
        {
            this._name = new Name("Testing");
            this._startDate = new PastDate(DateTime.UtcNow);
            this._endDate = new PastDate(DateTime.UtcNow);

            this._e = new UnsafeEra(
                this._name,
                null,
                10,
                20,
                30,
                this._startDate,
                this._endDate
            );
        }

        [Test]
        public void TestDefaultValues()
        {
            var name = new Name("Pumpkin Pie");
            var e = new UnsafeEra(name);

            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                e.StartDate.Get.ToString("MM:dd:yyyy HH")
            );
            Assert.AreEqual(new Level(2), e.TaskLevelBuffer);
            Assert.AreEqual(20, e.MaxPraxises);
            Assert.AreEqual(1, e.MaxTaskCompletion);
            Assert.AreEqual(2, e.MaxTaskCompletionReiterator);
            Assert.IsNull(e.EndDate);
        }

        [Test]
        public void TestDTOConstructor()
        {
            var name = new Name("Pumpkin Pie");
            var startDate = new PastDate(DateTime.UtcNow);
            var endDate = new PastDate(DateTime.UtcNow);
            var e = new UnsafeEra(new EraDTO(
                name,
                startDate,
                endDate,
                new Level(5),
                100,
                2,
                9
            ));
            Assert.IsNotNull(e.EndDate);
            Assert.AreEqual(new Level(5), e.TaskLevelBuffer);
            Assert.AreEqual(100, e.MaxPraxises);
            Assert.AreEqual(2, e.MaxTaskCompletion);
            Assert.AreEqual(9, e.MaxTaskCompletionReiterator);
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                e.EndDate.Get.ToString("MM:dd:yyyy HH")
            );
        }

        [Test]
        public void TestCustomValues()
        {
            var name = new Name("Pumpkin Pie");
            var startDate = new PastDate(DateTime.UtcNow);
            var endDate = new PastDate(DateTime.UtcNow);
            var e = new UnsafeEra(
                name,
                new Level(5),
                100,
                2,
                9,
                startDate,
                endDate
            );
            Assert.IsNotNull(e.EndDate);
            Assert.AreEqual(new Level(5), e.TaskLevelBuffer);
            Assert.AreEqual(100, e.MaxPraxises);
            Assert.AreEqual(2, e.MaxTaskCompletion);
            Assert.AreEqual(9, e.MaxTaskCompletionReiterator);
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
            Assert.Throws<ArgumentException>(()=>
                this._e.EndDate = new PastDate(new DateTime(1, 1, 1)));
            Assert.Throws<ArgumentException>(()=>
                this._e.StartDate = new PastDate(new DateTime(3000, 1, 1)));
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
            this._e.MaxTaskCompletion = 2;
            this._e.MaxTaskCompletionReiterator = 2;
            this._e.MaxTaskCompletion = 1;
            Assert.Throws<ArgumentException>(()=>this._e.MaxTaskCompletion = 0);
            Assert.Throws<ArgumentException>(()=>this._e.MaxTaskCompletion = 1000);
        }

        [Test]
        public void TestMaxTasksReiterator()
        {
            this._e.MaxTaskCompletion = 1;
            this._e.MaxTaskCompletionReiterator = 2;
            this._e.MaxTaskCompletionReiterator = 1;
            Assert.Throws<ArgumentException>(()=>
                this._e.MaxTaskCompletionReiterator = 0);
            this._e.MaxTaskCompletionReiterator = 100;
            this._e.MaxTaskCompletion = 100;
            Assert.Throws<ArgumentException>(()=>
                this._e.MaxTaskCompletionReiterator = 10);
        }
    }
}