using System;
using System.Collections.Generic;
using WorldZero.Common.Collections;
using WorldZero.Data.Interface.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity
{
    [TestFixture]
    public class TestEntityData
    {
        private int _ruleCount;
        private EntityData _data;

        [SetUp]
        public void Setup()
        {
            this._ruleCount = 2;
            this._data = new EntityData(this._ruleCount);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentException>(()=>new EntityData(-1));
            new EntityData(0);

            Assert.IsNotNull(this._data.Saved);
            Assert.IsNotNull(this._data.Staged);
            Assert.IsNotNull(this._data.SavedRules);
            Assert.AreEqual(this._ruleCount, this._data.SavedRules.Count);
            Assert.IsNotNull(this._data.StagedRules);
            Assert.AreEqual(this._ruleCount, this._data.StagedRules.Count);
            Assert.IsNotNull(this._data.RecycledRules);
            Assert.AreEqual(this._ruleCount, this._data.RecycledRules.Count);
        }

        [Test]
        public void TestSaved()
        {
            var temp = new Dictionary<object, object>();
            this._data.Saved = temp;
            Assert.AreEqual(temp, this._data.Saved);

            Assert.Throws<ArgumentNullException>(()=>this._data.Saved = null);
        }

        [Test]
        public void TestStaged()
        {
            var temp = new Dictionary<object, object>();
            this._data.Staged = temp;
            Assert.AreEqual(temp, this._data.Staged);

            Assert.Throws<ArgumentNullException>(()=>this._data.Staged = null);
        }

        [Test]
        public void TestSavedRules()
        {
            var temp = new W0List<Dictionary<W0Set<object>, object>>();
            this._data.SavedRules = temp;
            Assert.AreEqual(temp, this._data.SavedRules);

            Assert.Throws<ArgumentNullException>(()=>
                this._data.SavedRules = null);
        }

        [Test]
        public void TestStagedRules()
        {
            var temp = new W0List<Dictionary<W0Set<object>, object>>();
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
        public void TestCleanStaged()
        {
            this._data.Saved.Add(3, null);
            this._data.SavedRules[0].Add(new W0Set<object>(), 0);
            this._data.Staged.Add(9, null);
            this._data.StagedRules[1].Add(new W0Set<object>(), 9);
            this._data.RecycledRules[0].Add(new W0Set<object>(), -324);
            this._data.CleanStaged();
            Assert.IsNull(this._data.Saved[3]);
            Assert.AreEqual(1, this._data.SavedRules[0].Count);
            Assert.AreEqual(0, this._data.Staged.Count);
            Assert.AreEqual(0, this._data.StagedRules[1].Count);
            Assert.AreEqual(0, this._data.RecycledRules[0].Count);
        }

        [Test]
        public void TestClean()
        {
            this._data.Saved.Add(3, null);
            this._data.SavedRules[0].Add(new W0Set<object>(), 0);
            this._data.Staged.Add(9, null);
            this._data.StagedRules[1].Add(new W0Set<object>(), 9);
            this._data.RecycledRules[0].Add(new W0Set<object>(), -324);
            this._data.Clean();
            Assert.AreEqual(0, this._data.Saved.Count);
            Assert.AreEqual(0, this._data.SavedRules[0].Count);
            Assert.AreEqual(0, this._data.Staged.Count);
            Assert.AreEqual(0, this._data.StagedRules[1].Count);
            Assert.AreEqual(0, this._data.RecycledRules[0].Count);
        }
    }
}