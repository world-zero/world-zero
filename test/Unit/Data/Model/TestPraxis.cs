using WorldZero.Data.Model;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestPraxis
    {
        private Praxis _p;
        private int _praxisId;
        private int _taskId;
        private string _statusName;

        [SetUp]
        public void Setup()
        {
            this._praxisId = 19999;
            this._taskId = 939;
            // AreDueling is weird so it is not set here.
            this._statusName = "incomplete";

            this._p = new Praxis();
            this._p.PraxisId = this._praxisId;
            this._p.TaskId = this._taskId;
            this._p.StatusName = this._statusName;
        }

        [Test]
        public void TestDefaultValues()
        {
            var p = new Praxis();
            Assert.AreEqual(p.PraxisId, 0);
            Assert.IsNull(p.TaskId);
            Assert.IsNull(p.Task);
            Assert.IsFalse(p.AreDueling);
            Assert.IsNull(p.Status);
        }

        [Test]
        public void TestAreDueling()
        {
            Assert.IsNull(this._p.Collaborators);
            this._p.AreDueling = true;
            Assert.IsFalse(this._p.AreDueling);

            this._p.Collaborators = new HashSet<Character>();
            this._p.AreDueling = true;
            Assert.IsFalse(this._p.AreDueling);

            this._p.Collaborators.Add(new Character());
            this._p.AreDueling = true;
            Assert.IsFalse(this._p.AreDueling);

            this._p.Collaborators.Add(new Character());
            this._p.AreDueling = true;
            Assert.IsTrue(this._p.AreDueling);
            this._p.AreDueling = false;
            Assert.IsFalse(this._p.AreDueling);

            this._p.Collaborators.Add(new Character());
            this._p.AreDueling = true;
            Assert.IsFalse(this._p.AreDueling);
        }

        [Test]
        public void TestPraxisId()
        {
            Assert.AreEqual(this._praxisId, this._p.PraxisId);
            this._p.PraxisId = 0;
            Assert.AreEqual(0, this._p.PraxisId);
            Assert.Throws<ArgumentException>(()=>this._p.PraxisId = -1);
            Assert.AreEqual(0, this._p.PraxisId);
        }

        [Test]
        public void TestTaskId()
        {
            Assert.AreEqual(this._taskId, this._p.TaskId);
            this._p.TaskId = 0;
            Assert.AreEqual(0, this._p.TaskId);
            Assert.Throws<ArgumentException>(()=>this._p.TaskId = -1);
            Assert.AreEqual(0, this._p.TaskId);
            this._p.TaskId = null;
            Assert.IsNull(this._p.TaskId);
        }

        [Test]
        public void TestStatusName()
        {
            Assert.AreEqual(this._statusName, this._p.StatusName);
            this._p.StatusName = "Test";
            Assert.AreEqual("Test", this._p.StatusName);
            Assert.Throws<ArgumentException>(()=>this._p.StatusName = null);
            Assert.Throws<ArgumentException>(()=>this._p.StatusName = "");
            Assert.Throws<ArgumentException>(()=>this._p.StatusName = "     ");
            Assert.AreEqual("Test", this._p.StatusName);
        }
    }
}