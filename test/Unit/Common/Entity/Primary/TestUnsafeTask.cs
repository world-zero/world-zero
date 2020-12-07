using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;
using System;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestUnsafeTask
    {
        private Id _taskId;
        private string _summary;
        private PointTotal _points;
        private Level _level;
        private Level _minLevel;
        private Name _factionId;
        private Name _statusId;
        private UnsafeTask _t;

        [SetUp]
        public void Setup()
        {
            this._taskId = new Id(9001);
            this._summary = "A task";
            this._points = new PointTotal(1000);
            this._level = new Level(5);
            this._minLevel = new Level(3);
            this._factionId = new Name("foo");
            this._statusId = new Name("bar");

            this._t = new UnsafeTask(
                this._taskId,
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
        }

        [Test]
        public void TestDefaultValues()
        {
            var t = new UnsafeTask(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level
            );
            Assert.AreEqual(new Id(0), t.Id);
            Assert.AreEqual(this._factionId, t.FactionId);
            Assert.AreEqual(this._statusId, t.StatusId);
            Assert.AreEqual(this._summary, t.Summary);
            Assert.AreEqual(this._points, t.Points);
            Assert.AreEqual(this._level, t.Level);
            Assert.AreEqual(new Level(0), t.MinLevel);

            t = new UnsafeTask(
                this._factionId,
                this._statusId,
                this._summary,
                this._points,
                this._level,
                this._minLevel
            );
            Assert.AreEqual(new Id(0), t.Id);
            Assert.AreEqual(this._factionId, t.FactionId);
            Assert.AreEqual(this._statusId, t.StatusId);
            Assert.AreEqual(this._summary, t.Summary);
            Assert.AreEqual(this._points, t.Points);
            Assert.AreEqual(this._level, t.Level);
            Assert.AreEqual(this._minLevel, t.MinLevel);
        }

        [Test]
        public void TestPoints()
        {
            Assert.AreEqual(this._points, this._t.Points);
            this._t.Points = new PointTotal(100000);
            Assert.AreEqual(new PointTotal(100000), this._t.Points);
            Assert.Throws<ArgumentNullException>(()=>this._t.Points = null);
            Assert.AreEqual(new PointTotal(100000), this._t.Points);
        }

        [Test]
        public void TestLevel()
        {
            Assert.AreEqual(this._level, this._t.Level);
            this._t.Level = new Level(9);
            Assert.AreEqual(new Level(9), this._t.Level);
            Assert.Throws<ArgumentNullException>(()=>this._t.Level = null);
            Assert.AreEqual(new Level(9), this._t.Level);
        }

        [Test]
        public void TestMinLevel()
        {
            Assert.AreEqual(this._minLevel, this._t.MinLevel);
            this._t.MinLevel = new Level(0);
            Assert.AreEqual(new Level(0), this._t.MinLevel);
            Assert.Throws<ArgumentNullException>(()=>this._t.MinLevel = null);
            Assert.AreEqual(new Level(0), this._t.MinLevel);
        }

        [Test]
        public void TestLevels()
        {
            Assert.AreEqual(this._level, this._t.Level);
            Assert.AreEqual(this._minLevel, this._t.MinLevel);
            this._t.MinLevel = new Level(1);
            this._t.Level = new Level(1);
            Assert.Throws<ArgumentException>(()=>this._t.MinLevel = new Level(2));
            Assert.Throws<ArgumentException>(()=>this._t.Level = new Level(0));
        }

        [Test]
        public void TestFactionId()
        {
            Assert.AreEqual(this._factionId, this._t.FactionId);
            this._t.FactionId = new Name("kennel");
            Assert.AreEqual(new Name("kennel"), this._t.FactionId);
            Assert.Throws<ArgumentNullException>(()=>this._t.FactionId = null);
            Assert.AreEqual(new Name("kennel"), this._t.FactionId);
        }

        [Test]
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._t.StatusId);
            this._t.StatusId = new Name("crate");
            Assert.AreEqual(new Name("crate"), this._t.StatusId);
            Assert.Throws<ArgumentNullException>(()=>this._t.StatusId = null);
            Assert.AreEqual(new Name("crate"), this._t.StatusId);
        }
    }
}