using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestMetaTask
    {
        private Id _metaTaskId;
        private string _desc;
        private double _bonus;
        private bool _isFlatBonus;
        private Name _factionId;
        private Name _statusId;
        private MetaTask _mt;

        [SetUp]
        public void Setup()
        {
            this._metaTaskId = new Id(19);
            this._desc = "Complete this task in under 30 minutes.";
            this._bonus = 50;
            this._isFlatBonus = true;
            this._factionId = new Name("a faction");
            this._statusId = new Name("valid");

            this._mt = new MetaTask(this._metaTaskId, this._factionId, this._statusId, this._desc, this._bonus, this._isFlatBonus);
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(this._metaTaskId, this._mt.Id);
            Assert.AreEqual(this._factionId, this._mt.FactionId);
            Assert.AreEqual(this._statusId, this._mt.StatusId);
            Assert.AreEqual(this._bonus, this._mt.Bonus);
            Assert.AreEqual(this._isFlatBonus, this._mt.IsFlatBonus);
        }

        [Test]
        public void TestBonus()
        {
            Assert.AreEqual(this._bonus, this._mt.Bonus);
            this._mt.Bonus = 9001;
            Assert.AreEqual(9001, this._mt.Bonus);
            Assert.Throws<ArgumentException>(()=>this._mt.Bonus = -1);
            Assert.Throws<ArgumentException>(()=>this._mt.Bonus = 0);
            Assert.AreEqual(9001, this._mt.Bonus);
            this._mt.Bonus = 1;
            Assert.AreEqual(1, this._mt.Bonus);
        }

        [Test]
        public void TestFactionId()
        {
            Assert.AreEqual(this._factionId, this._mt.FactionId);
            this._mt.FactionId = new Name("kennel");
            Assert.AreEqual(new Name("kennel"), this._mt.FactionId);
            Assert.Throws<ArgumentNullException>(()=>this._mt.FactionId = null);
            Assert.AreEqual(new Name("kennel"), this._mt.FactionId);
        }

        [Test]
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._mt.StatusId);
            this._mt.StatusId = new Name("crate");
            Assert.AreEqual(new Name("crate"), this._mt.StatusId);
            Assert.Throws<ArgumentNullException>(()=>this._mt.StatusId = null);
            Assert.AreEqual(new Name("crate"), this._mt.StatusId);
        }
    }
}