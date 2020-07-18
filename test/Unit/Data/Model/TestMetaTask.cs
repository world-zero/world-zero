using WorldZero.Common.Model;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestMetaTask
    {
        private MetaTask _mt;
        private int _metaTaskId;
        private string _metaTaskName;
        private string _desc;
        private double _bonus;
        private bool _isFlat;
        private string _factionName;
        private string _statusName;

        [SetUp]
        public void Setup()
        {
            this._metaTaskId = 19;
            this._metaTaskName = "< 30 minute speedrun";
            this._desc = "Complete this task in under 30 minutes.";
            this._bonus = 50;
            this._isFlat = true;
            this._factionName = "Speedrunners";
            this._statusName = "Valid";

            this._mt = new MetaTask();
            this._mt.MetaTaskId = this._metaTaskId;
            this._mt.MetaTaskName = this._metaTaskName;
            this._mt.Description = this._desc;
            this._mt.Bonus = this._bonus;
            this._mt.IsFlatBonus = this._isFlat;
            this._mt.FactionName = this._factionName;
            this._mt.StatusName = this._statusName;
        }

        [Test]
        public void TestDefaultValues()
        {
            var mt = new MetaTask();
            Assert.AreEqual(mt.MetaTaskId, 0);
            Assert.IsNull(mt.MetaTaskName);
            Assert.IsNull(mt.Description);
            Assert.AreEqual(mt.Bonus, 0);
            Assert.AreEqual(mt.IsFlatBonus, true);
            Assert.IsNull(mt.Faction);
            Assert.IsNull(mt.FactionName);
            Assert.IsNull(mt.Status);
            Assert.IsNull(mt.StatusName);
        }

        [Test]
        public void TestMetaTaskId()
        {
            Assert.AreEqual(this._metaTaskId, this._mt.MetaTaskId);
            this._mt.MetaTaskId = 0;
            Assert.AreEqual(0, this._mt.MetaTaskId);
            Assert.Throws<ArgumentException>(()=>this._mt.MetaTaskId = -1);
            Assert.AreEqual(0, this._mt.MetaTaskId);
        }

        [Test]
        public void TestMetaTaskName()
        {
            Assert.AreEqual(this._metaTaskName, this._mt.MetaTaskName);
            this._mt.MetaTaskName = "Test";
            Assert.AreEqual("Test", this._mt.MetaTaskName);
            Assert.Throws<ArgumentException>(()=>this._mt.MetaTaskName = null);
            Assert.Throws<ArgumentException>(()=>this._mt.MetaTaskName = "");
            Assert.Throws<ArgumentException>(()=>this._mt.MetaTaskName = "     ");
            Assert.AreEqual("Test", this._mt.MetaTaskName);
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
        public void TestFactionName()
        {
            Assert.AreEqual(this._factionName, this._mt.FactionName);
            this._mt.FactionName = "Test";
            Assert.AreEqual("Test", this._mt.FactionName);
            Assert.Throws<ArgumentException>(()=>this._mt.FactionName = null);
            Assert.Throws<ArgumentException>(()=>this._mt.FactionName = "");
            Assert.Throws<ArgumentException>(()=>this._mt.FactionName = "     ");
            Assert.AreEqual("Test", this._mt.FactionName);
        }

        [Test]
        public void TestStatusName()
        {
            Assert.AreEqual(this._statusName, this._mt.StatusName);
            this._mt.StatusName = "Test";
            Assert.AreEqual("Test", this._mt.StatusName);
            Assert.Throws<ArgumentException>(()=>this._mt.StatusName = null);
            Assert.Throws<ArgumentException>(()=>this._mt.StatusName = "");
            Assert.Throws<ArgumentException>(()=>this._mt.StatusName = "     ");
            Assert.AreEqual("Test", this._mt.StatusName);
        }
    }
}