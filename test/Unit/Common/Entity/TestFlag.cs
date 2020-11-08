using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestFlag
    {
        private Name _flagId;
        private string _desc;
        private double _penalty;
        private bool _isFlatPenalty;
        private Flag _f;

        [SetUp]
        public void Setup()
        {
            this._flagId = new Name("fishy");
            this._desc = "Smells like fish";
            this._penalty = 50;
            this._isFlatPenalty = true;

            this._f = new Flag(this._flagId, this._desc, this._penalty, this._isFlatPenalty);
        }

        [Test]
        public void TestDefaultValues()
        {
            var f = new Flag(new Name("x"));
            Assert.AreEqual(new Name("x"), f.Id);
            Assert.IsNull(f.Description);
            Assert.AreEqual(0.1, f.Penalty);
            Assert.AreEqual(false, f.IsFlatPenalty);
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(this._flagId, this._f.Id);
            Assert.AreEqual(this._desc, this._f.Description);
            Assert.AreEqual(this._penalty, this._f.Penalty);
            Assert.AreEqual(this._isFlatPenalty, this._f.IsFlatPenalty);
        }

        [Test]
        public void TestPenalty()
        {
            Assert.AreEqual(this._penalty, this._f.Penalty);
            this._f.Penalty = 9001;
            Assert.AreEqual(9001, this._f.Penalty);
            Assert.Throws<ArgumentException>(()=>this._f.Penalty = -1);
            Assert.Throws<ArgumentException>(()=>this._f.Penalty = 0);
            Assert.AreEqual(9001, this._f.Penalty);
            this._f.Penalty = 1;
            Assert.AreEqual(1, this._f.Penalty);
        }
    }
}