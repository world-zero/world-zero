using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestPraxis
    {
        private Id _praxisId;
        private Id _taskId;
        private Name _statusId;
        private bool _areDueling;
        private Praxis _p;

        [SetUp]
        public void Setup()
        {
            this._praxisId = new Id(19999);
            this._taskId = new Id(939);
            this._statusId = new Name("valid");
            this._areDueling = true;

            this._p = new Praxis(
                this._praxisId,
                this._taskId,
                this._statusId,
                this._areDueling
            );
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(this._praxisId, this._p.Id);
            Assert.AreEqual(this._taskId, this._p.TaskId);
            Assert.AreEqual(this._statusId, this._p.StatusId);
            Assert.AreEqual(this._areDueling, this._p.AreDueling);
        }

        [Test]
        public void TestTaskId()
        {
            Assert.AreEqual(this._taskId, this._p.TaskId);
            this._p.TaskId = new Id(34);
            Assert.AreEqual(new Id(34), this._p.TaskId);
            Assert.Throws<ArgumentException>(()=>this._p.TaskId = null);
            Assert.AreEqual(new Id(34), this._p.TaskId);
        }

        [Test]
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._p.StatusId);
            this._p.StatusId = new Name("crate");
            Assert.AreEqual(new Name("crate"), this._p.StatusId);
            Assert.Throws<ArgumentException>(()=>this._p.StatusId = null);
            Assert.AreEqual(new Name("crate"), this._p.StatusId);
        }
    }
}