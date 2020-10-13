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
        private EntityData _data;

        [SetUp]
        public void Setup()
        {
            this._data = new EntityData();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(this._data.Saved);
            Assert.IsNotNull(this._data.Staged);
            Assert.IsNotNull(this._data.SavedRules);
            Assert.IsNotNull(this._data.StagedRules);
            Assert.IsNotNull(this._data.RecycledRules);
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
    }
}