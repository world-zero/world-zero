using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestTask
    {
        private Task _t;
        private int _taskId;
        private string _summary;
        private int _points;
        private int _level;
        private int _minLevel;
        private string _factionId;
        private string _statusId;

        [SetUp]
        public void Setup()
        {
            this._taskId = 9001;
            this._summary = "A task";
            this._points = 1000;
            this._level = 5;
            this._minLevel = 3;
            this._factionId = "foo";
            this._statusId = "bar";

            this._t = new Task();
            this._t.Id = this._taskId;
            this._t.Summary = this._summary;
            this._t.Points = this._points;
            this._t.Level = this._level;
            this._t.MinLevel = this._minLevel;
            this._t.FactionId = this._factionId;
            this._t.StatusId = this._statusId;
        }

        [Test]
        public void TestDefaultValues()
        {
            var t = new Task();
            Assert.AreEqual(t.Id, 0);
            Assert.IsNull(t.Summary);
            Assert.AreEqual(t.Points, 0);
            Assert.AreEqual(t.Level, 0);
            Assert.AreEqual(t.MinLevel, 0);
            Assert.IsNull(t.Faction);
            Assert.IsNull(t.Status);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._taskId, this._t.Id);
            this._t.Id = 0;
            Assert.AreEqual(0, this._t.Id);
            Assert.Throws<ArgumentException>(()=>this._t.Id = -1);
            Assert.AreEqual(0, this._t.Id);
        }

        [Test]
        public void TestPoints()
        {
            Assert.AreEqual(this._points, this._t.Points);
            this._t.Points = 0;
            Assert.AreEqual(0, this._t.Points);
            Assert.Throws<ArgumentException>(()=>this._t.Points = -1);
            Assert.AreEqual(0, this._t.Points);
        }

        [Test]
        public void TestLevel()
        {
            Assert.AreEqual(this._level, this._t.Level);
            this._t.Level = 9;
            Assert.AreEqual(9, this._t.Level);
            Assert.Throws<ArgumentException>(()=>this._t.Level = -1);
            Assert.AreEqual(9, this._t.Level);
        }

        [Test]
        public void TestMinLevel()
        {
            Assert.AreEqual(this._minLevel, this._t.MinLevel);
            this._t.MinLevel = 0;
            Assert.AreEqual(0, this._t.MinLevel);
            Assert.Throws<ArgumentException>(()=>this._t.MinLevel = -1);
            Assert.AreEqual(0, this._t.MinLevel);
        }

        [Test]
        public void TestLevels()
        {
            Assert.AreEqual(this._level, this._t.Level);
            Assert.AreEqual(this._minLevel, this._t.MinLevel);
            this._t.MinLevel = 1;
            this._t.Level = 1;
            Assert.Throws<ArgumentException>(()=>this._t.MinLevel = 2);
            Assert.Throws<ArgumentException>(()=>this._t.Level = 0);
        }

        [Test]
        public void TestFactionId()
        {
            Assert.AreEqual(this._factionId, this._t.FactionId);
            this._t.FactionId = "kennel";
            Assert.AreEqual("kennel", this._t.FactionId);
            Assert.Throws<ArgumentException>(()=>this._t.FactionId = null);
            Assert.Throws<ArgumentException>(()=>this._t.FactionId = "");
            Assert.Throws<ArgumentException>(()=>this._t.FactionId = "   ");
            Assert.AreEqual("kennel", this._t.FactionId);
        }

        [Test]
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._t.StatusId);
            this._t.StatusId = "crate";
            Assert.AreEqual("crate", this._t.StatusId);
            Assert.Throws<ArgumentException>(()=>this._t.StatusId = null);
            Assert.Throws<ArgumentException>(()=>this._t.StatusId = "");
            Assert.Throws<ArgumentException>(()=>this._t.StatusId = "   ");
            Assert.AreEqual("crate", this._t.StatusId);
        }
    }
}