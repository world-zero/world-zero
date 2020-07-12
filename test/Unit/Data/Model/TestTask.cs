using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
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
        private string _factionName;
        private string _statusName;

        [SetUp]
        public void Setup()
        {
            this._taskId = 9001;
            this._summary = "A task";
            this._points = 1000;
            this._level = 5;
            this._minLevel = 3;
            this._factionName = "DIO";
            this._statusName = "SUCCESS";

            this._t = new Task();
            this._t.TaskId = this._taskId;
            this._t.Summary = this._summary;
            this._t.Points = this._points;
            this._t.Level = this._level;
            this._t.MinLevel = this._minLevel;
            this._t.FactionName = this._factionName;
            this._t.StatusName = this._statusName;
        }

        [Test]
        public void TestDefaultValues()
        {
            var t = new Task();
            Assert.AreEqual(t.TaskId, 0);
            Assert.IsNull(t.Summary);
            Assert.AreEqual(t.Points, 0);
            Assert.AreEqual(t.Level, 0);
            Assert.AreEqual(t.MinLevel, 0);
            Assert.IsNull(t.Faction);
            Assert.IsNull(t.Status);
        }

        [Test]
        public void TestTaskId()
        {
            Assert.AreEqual(this._taskId, this._t.TaskId);
            this._t.TaskId = 0;
            Assert.AreEqual(0, this._t.TaskId);
            Assert.Throws<ArgumentException>(()=>this._t.TaskId = -1);
            Assert.AreEqual(0, this._t.TaskId);
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
        public void TestFactionName()
        {
            Assert.AreEqual(this._factionName, this._t.FactionName);
            this._t.FactionName = "Test";
            Assert.AreEqual("Test", this._t.FactionName);
            Assert.Throws<ArgumentException>(()=>this._t.FactionName = "");
            Assert.Throws<ArgumentException>(()=>this._t.FactionName = "     ");
            Assert.AreEqual("Test", this._t.FactionName);
        }

        [Test]
        public void TestStatusName()
        {
            Assert.AreEqual(this._statusName, this._t.StatusName);
            this._t.StatusName = "Test";
            Assert.AreEqual("Test", this._t.StatusName);
            Assert.Throws<ArgumentException>(()=>this._t.StatusName = null);
            Assert.Throws<ArgumentException>(()=>this._t.StatusName = "");
            Assert.Throws<ArgumentException>(()=>this._t.StatusName = "     ");
            Assert.AreEqual("Test", this._t.StatusName);
        }
    }
}