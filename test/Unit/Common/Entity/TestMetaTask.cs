using WorldZero.Common.Entity;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestMetaTask
    {
        private MetaTask _mt;
        private int _metaTaskId;
        private string _desc;
        private double _bonus;
        private bool _isFlat;
        private string _factionId;
        private string _statusId;

        [SetUp]
        public void Setup()
        {
            this._metaTaskId = 19;
            this._desc = "Complete this task in under 30 minutes.";
            this._bonus = 50;
            this._isFlat = true;
            this._factionId = "a faction";
            this._statusId = "valid";

            this._mt = new MetaTask();
            this._mt.Id = this._metaTaskId;
            this._mt.Description = this._desc;
            this._mt.Bonus = this._bonus;
            this._mt.IsFlatBonus = this._isFlat;
            this._mt.FactionId = this._factionId;
            this._mt.StatusId = this._statusId;
        }

        [Test]
        public void TestDefaultValues()
        {
            var mt = new MetaTask();
            Assert.AreEqual(mt.Id, 0);
            Assert.IsNull(mt.Description);
            Assert.AreEqual(mt.Bonus, 0);
            Assert.AreEqual(mt.IsFlatBonus, true);
            Assert.IsNull(mt.Faction);
            Assert.IsNull(mt.FactionId);
            Assert.IsNull(mt.Status);
            Assert.IsNull(mt.StatusId);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._metaTaskId, this._mt.Id);
            this._mt.Id = 0;
            Assert.AreEqual(0, this._mt.Id);
            Assert.Throws<ArgumentException>(()=>this._mt.Id = -1);
            Assert.AreEqual(0, this._mt.Id);
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
            this._mt.FactionId = "asdf";
            Assert.AreEqual("asdf", this._mt.FactionId);
            Assert.Throws<ArgumentException>(()=>this._mt.FactionId = null);
            Assert.Throws<ArgumentException>(()=>this._mt.FactionId = "");
            Assert.Throws<ArgumentException>(()=>this._mt.FactionId = "    ");
            Assert.AreEqual("asdf", this._mt.FactionId);
        }

        [Test]
        public void TestStatusId()
        {
            Assert.AreEqual(this._statusId, this._mt.StatusId);
            this._mt.StatusId = "asdffdsa";
            Assert.AreEqual("asdffdsa", this._mt.StatusId);
            Assert.Throws<ArgumentException>(()=>this._mt.StatusId = "");
            Assert.Throws<ArgumentException>(()=>this._mt.StatusId = "    ");
            Assert.AreEqual("asdffdsa", this._mt.StatusId);

            this._mt.StatusId = null;
            Assert.IsNull(this._mt.StatusId);
        }
    }
}