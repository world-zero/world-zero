using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Collections;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity
{
    // NOTE: TestIRAMEntityRepo.cs does not test the functional member
    // `GenerateId`, and that is done here instead. Additionally, the tests
    // around FinalChecks() are performed here by the same logic.
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

        private void _assertEntitiesEqual(Task expected, Task actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FactionId, actual.FactionId);
            Assert.AreEqual(expected.StatusId, actual.StatusId);
            Assert.AreEqual(expected.Summary, actual.Summary);
            Assert.AreEqual(expected.Points, actual.Points);
            Assert.AreEqual(expected.Level, actual.Level);
            Assert.AreEqual(expected.MinLevel, actual.MinLevel);
        }

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

        [TearDown]
        public void TearDown()
        {
            this._repo.Clean();
            this._repo.ResetNextIdValue();
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

        [Test]
        public void TestFinalChecks()
        {
            var repo = new TestRAMIdEntityRepoBROKEN();
            var e = new Task(
                new Name("f"),
                new Name("invalid"),
                "x",
                new PointTotal(43),
                new Level(2)
            );
            repo.Insert(e);
            Assert.Throws<ArgumentException>(()=>repo.Save());
            Assert.IsFalse(e.IsIdSet());
            Assert.AreEqual(0, repo.Saved.Count);
            Assert.AreEqual(0, repo.Staged.Count);
            for (int i = 0; i < repo.SavedRules.Count; i++)
            {
                Assert.AreEqual(0, repo.SavedRules[i].Count);
                Assert.AreEqual(0, repo.StagedRules[i].Count);
                Assert.AreEqual(0, repo.RecycledRules[i].Count);
            }
        }
    }

    public class TestRAMIdEntityRepo
        : IRAMIdEntityRepo<Task>
    {
        public void ResetNextIdValue() { _nextIdValue = 1; }

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

    public class TestRAMIdEntityRepoBROKEN
        : IRAMIdEntityRepo<Task>
    {
        private bool _isFirstFinalCheck = true;

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

        protected override void FinalChecks()
        {
            if (this._isFirstFinalCheck)
            {
                this._isFirstFinalCheck = !this._isFirstFinalCheck;
                throw new ArgumentException("foo");
            }
            else
            {
                this._isFirstFinalCheck = !this._isFirstFinalCheck;
            }
        }

        public Dictionary<object, object> Saved
        { get { return this._saved; } }
        public Dictionary<object, object> Staged
        { get{ return this._staged; } }
        public W0List<Dictionary<W0Set<object>, object>> SavedRules
        { get { return this._savedRules; } }
        public W0List<Dictionary<W0Set<object>, object>> StagedRules
        { get { return this._stagedRules; } }
        public W0List<Dictionary<W0Set<object>, int>> RecycledRules
        { get { return this._recycledRules; } }
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