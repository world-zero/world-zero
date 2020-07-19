using WorldZero.Common.Entity;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestPraxis
    {
        private Praxis _p;
        private int _praxisId;
        private int _taskId;
        private int _statusId;

        [SetUp]
        public void Setup()
        {
            this._praxisId = 19999;
            this._taskId = 939;
            // AreDueling is weird so it is not set here.
            this._statusId = 4323;

            this._p = new Praxis();
            this._p.Id = this._praxisId;
            this._p.TaskId = this._taskId;
            this._p.StatusId = this._statusId;
        }

        [Test]
        public void TestDefaultValues()
        {
            var p = new Praxis();
            Assert.AreEqual(p.Id, 0);
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
        public void TestId()
        {
            Assert.AreEqual(this._praxisId, this._p.Id);
            this._p.Id = 0;
            Assert.AreEqual(0, this._p.Id);
            Assert.Throws<ArgumentException>(()=>this._p.Id = -1);
            Assert.AreEqual(0, this._p.Id);
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
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._p.StatusId);
            this._p.StatusId = 0;
            Assert.AreEqual(0, this._p.StatusId);
            Assert.Throws<ArgumentException>(()=>this._p.StatusId = -1);
            Assert.AreEqual(0, this._p.StatusId);
        }
    }
}