using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Collections;
using WorldZero.Data.Interface.Repository.Entity.RAM;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM
{
    // NOTE: TestIRAMEntityRepo.cs does not test the functional member
    // `GenerateId`, and that is done here instead.
    [TestFixture]
    public class TestIRAMIdEntityRepo
    {
        private Name _factionId;
        private Name _statusId;
        private string _summary;
        private PointTotal _points;
        private Level _level;
        private Level _minLevel;
        private Task _task;
        private TestRAMIdEntityRepo _repo;

        [SetUp]
        public void Setup()
        {
            this._factionId = new Name("Hal's Halberds");
            this._statusId = new Name("VALID");
            this._summary = "something";
            this._points = new PointTotal(100000);
            this._level = new Level(5);
            this._minLevel = new Level(3);

            this._task = new Task(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
            this._repo = new TestRAMIdEntityRepo();
        }

        [Test]
        public void TestThatIdIsSetOnSave()
        {
            Assert.IsFalse(this._task.IsIdSet());
            this._repo.Insert(this._task);
            Assert.IsFalse(this._task.IsIdSet());
            this._repo.Save();
            Assert.IsTrue(this._task.IsIdSet());
            Assert.AreEqual(new Id(1), this._task.Id);

            // It doesn't matter that these tasks are basically the same, they
            // will have different IDs.
            var task1 = new Task(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
            var task2 = new Task(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
            this._repo.Insert(task1);
            this._repo.Insert(task2);
            this._repo.Save();
            Assert.IsTrue(task1.IsIdSet());
            Assert.AreEqual(new Id(2), task1.Id);
            Assert.IsTrue(task2.IsIdSet());
            Assert.AreEqual(new Id(3), task2.Id);
        }

        [Test]
        public void TestIdIsntReSetOnSave()
        {
            var task2 = new Task(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
            this._repo.Insert(task2);
            this._repo.Save();
            Assert.AreEqual(new Id(1), task2.Id);

            task2.StatusId = new Name("INVALID");
            this._repo.Update(task2);
            this._repo.Save();
            Assert.AreEqual(new Id(1), task2.Id);
        }

        [Test]
        public void TestIdsArentArtifactsOnSaveFailure()
        {
            var repo = new TestTaskRAMIdEntityRepo();
            var validTask = new TestTask(
                new Name("valid"),
                new Name("f"),
                "x",
                new PointTotal(43),
                new Level(2),
                3
            );
            repo.Insert(validTask);
            var invalidTask = new TestTask(
                new Name("invalid"),
                new Name("f"),
                "x",
                new PointTotal(43),
                new Level(2),
                3
            );
            repo.Insert(invalidTask);
            Assert.Throws<ArgumentException>(()=>repo.Save());
            Assert.IsFalse(validTask.IsIdSet());
        }
    }

    public class TestRAMIdEntityRepo
        : IRAMIdEntityRepo<Task>
    {
        protected override int GetRuleCount()
        {
            var a = new Task(
                new Name("f"),
                new Name("x"),
                "z",
                new PointTotal(2),
                new Level(3)
            );
            return a.GetUniqueRules().Count;
        }
    }

    public class TestTaskRAMIdEntityRepo
        : IRAMIdEntityRepo<TestTask>
    {
        protected override int GetRuleCount()
        {
            var a = new TestTask(
                new Name("f"),
                new Name("x"),
                "z",
                new PointTotal(2),
                new Level(3),
                3
            );
            return a.GetUniqueRules().Count;
        }
    }

    public class TestTask
        : Task
    {
        public TestTask(
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            int unique,
            Level minLevel=null,
            bool isHistorianable=false
        )
        : base (
            factionId,
            statusId,
            summary,
            points,
            level,
            minLevel,
            isHistorianable
        )
        {
            this.Unique = unique;
        }

        public TestTask(
            Id id,
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            int unique,
            Level minLevel=null,
            bool isHistorianable=false
        )
        : base (
            id,
            factionId,
            statusId,
            summary,
            points,
            level,
            minLevel,
            isHistorianable
        )
        {
            this.Unique = unique;
        }

        public override IEntity<Id, int> Clone()
        {
            return new TestTask(
                this.Id,
                this.FactionId,
                this.StatusId,
                this.Summary,
                this.Points,
                this.Level,
                this.Unique,
                this.MinLevel,
                this.isHistorianable
            );
        }

        public int Unique { get; set; }

        internal override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            var n = new W0Set<object>();
            n.Add(this.Unique);
            r.Add(n);
            return r;
        }
    }
}