using System;
using System.Collections.Generic;
using WorldZero.Common.Collections;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity
{
    [TestFixture]
    public class TestEntityData
    {
        private int _ruleCount;
        private EntityData<Id, int, Player> _data;

        [SetUp]
        public void Setup()
        {
            this._ruleCount = 1;
            this._data = new EntityData<Id, int, Player>(this._ruleCount);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(this._data.Saved);
            Assert.IsNotNull(this._data.Staged);
            Assert.IsNotNull(this._data.SavedRules);
            Assert.IsNotNull(this._data.StagedRules);
            Assert.IsNotNull(this._data.RecycledRules);
            Assert.AreEqual(this._ruleCount, this._data.RuleCount);
        }

        [Test]
        public void TestSaved()
        {
            var temp = new Dictionary<Id, Player>();
            this._data.Saved = temp;
            Assert.AreEqual(temp, this._data.Saved);

            Assert.Throws<ArgumentNullException>(()=>this._data.Saved = null);
        }

        [Test]
        public void TestStaged()
        {
            var temp = new Dictionary<Id, Player>();
            this._data.Staged = temp;
            Assert.AreEqual(temp, this._data.Staged);

            Assert.Throws<ArgumentNullException>(()=>this._data.Staged = null);
        }

        [Test]
        public void TestSavedRules()
        {
            var temp = new W0List<Dictionary<W0Set<object>, Player>>();
            this._data.SavedRules = temp;
            Assert.AreEqual(temp, this._data.SavedRules);

            Assert.Throws<ArgumentNullException>(()=>
                this._data.SavedRules = null);
        }

        [Test]
        public void TestStagedRules()
        {
            var temp = new W0List<Dictionary<W0Set<object>, Player>>();
            this._data.StagedRules = temp;
            Assert.AreEqual(temp, this._data.StagedRules);

            Assert.Throws<ArgumentNullException>(()=>
                this._data.StagedRules = null);
        }

        [Test]
        public void TestRecycledRules()
        {
            var temp = new W0List<Dictionary<W0Set<object>, int>>();
            this._data.RecycledRules = temp;
            Assert.AreEqual(temp, this._data.RecycledRules);

            Assert.Throws<ArgumentNullException>(()=>
                this._data.RecycledRules = null);
        }

        [Test]
        public void TestRuleCount()
        {
            var temp = 7;
            this._data.RuleCount = temp;
            Assert.AreEqual(temp, this._data.RuleCount);

            Assert.Throws<ArgumentException>(()=>this._data.RuleCount = -1);
            this._data.RuleCount = 0;
            this._data.RuleCount = 1;
        }
    }
}