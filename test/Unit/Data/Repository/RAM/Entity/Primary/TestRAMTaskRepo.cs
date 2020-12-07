using System;
using System.Linq;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMTaskRepo
    {
        private RAMTaskRepo _taskRepo;
        private Name _f0;
        private Name _f1;
        private Task _t0_0;
        private Task _t0_1;
        private Task _t1_0;

        [SetUp]
        public void Setup()
        {
            this._taskRepo = new RAMTaskRepo();
            var status = new Name("x");
            var pt = new PointTotal(2);
            var level = new Level(2);
            this._f0 = new Name("first");
            this._f1 = new Name("second");
            this._t0_0 = new Task(this._f0, status, "x", pt, level);
            this._t0_1 = new Task(this._f0, status, "a", pt, level);
            this._t1_0 = new Task(this._f1, status, "y", pt, level);
            this._taskRepo.Insert(this._t0_0);
            this._taskRepo.Insert(this._t0_1);
            this._taskRepo.Insert(this._t1_0);
            this._taskRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._taskRepo.CleanAll();
        }

        [Test]
        public void TestGetByFactionIdSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._taskRepo.GetByFactionId(null));
            Assert.Throws<ArgumentException>(()=>
                this._taskRepo.GetByFactionId(new Name("fake")));
        }

        [Test]
        public void TestGetByFactionId_f0()
        {
            var tasks = this._taskRepo.GetByFactionId(this._f0).ToList();
            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual(this._t0_0.Id, tasks[0].Id);
            Assert.AreEqual(this._t0_1.Id, tasks[1].Id);
        }

        [Test]
        public void TestGetByFactionId_f1()
        {
            var tasks = this._taskRepo.GetByFactionId(this._f1).ToList();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual(this._t1_0.Id, tasks[0].Id);
        }
    }
}