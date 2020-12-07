using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TestUnsafePraxisParticipant
    {
        private Id _id0;
        private Id _id1;
        private UnsafePraxisParticipant _pp;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._pp = new UnsafePraxisParticipant(this._id0, this._id1);
        }

        [Test]
        public void TestPraxisId()
        {
            var newId0 = new Id(0);

            this._pp.PraxisId = null;
            Assert.AreEqual(null, this._pp.PraxisId);
            Assert.AreEqual(newId0, this._pp.LeftId);

            this._pp.PraxisId = newId0;
            Assert.AreEqual(null, this._pp.PraxisId);
            Assert.AreEqual(newId0, this._pp.LeftId);

            this._pp.PraxisId = new Id(5);
            Assert.AreEqual(new Id(5), this._pp.PraxisId);
            Assert.AreEqual(new Id(5), this._pp.LeftId);
        }

        [Test]
        public void TestSpecialConstructor()
        {
            var pp = new UnsafePraxisParticipant(new Id(2));
            Assert.AreEqual(null, pp.PraxisId);
            Assert.AreEqual(new Id(0), pp.LeftId);
        }
    }
}