using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestUnsafeFlag
    {
        private Name _flagId;
        private string _desc;
        private PointTotal _penalty;
        private bool _isFlatPenalty;
        private UnsafeFlag _f;

        [SetUp]
        public void Setup()
        {
            this._flagId = new Name("fishy");
            this._desc = "Smells like fish";
            this._penalty = new PointTotal(50);
            this._isFlatPenalty = true;

            this._f = new UnsafeFlag(this._flagId, this._desc, this._penalty, this._isFlatPenalty);
        }

        [Test]
        public void TestDefaultValues()
        {
            var f = new UnsafeFlag(new Name("x"));
            Assert.AreEqual(new Name("x"), f.Id);
            Assert.IsNull(f.Description);
            Assert.AreEqual(new PointTotal(0.1), f.Penalty);
            Assert.AreEqual(false, f.IsFlatPenalty);
        }

        [Test]
        public void TestDTOConstructor()
        {
            this._f = new UnsafeFlag(new FlagDTO(
                this._flagId,
                this._desc,
                this._isFlatPenalty,
                this._penalty
            ));
            Assert.AreEqual(this._flagId, this._f.Id);
            Assert.AreEqual(this._desc, this._f.Description);
            Assert.AreEqual(this._penalty, this._f.Penalty);
            Assert.AreEqual(this._isFlatPenalty, this._f.IsFlatPenalty);
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
            Assert.Throws<ArgumentNullException>(()=>this._f.Penalty = null);
        }
    }
}