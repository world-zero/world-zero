using System;
using WorldZero.Common.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestVote
    {
        private Vote _v;
        private int _praxisId;
        private int _characterId;
        private int _points;

        [SetUp]
        public void Setup()
        {
            this._characterId = 19;
            this._praxisId = 33;
            this._points = 1000;

            this._v = new Vote();
            this._v.PraxisId = this._praxisId;
            this._v.CharacterId = this._characterId;
            this._v.Points = this._points;
        }

        [Test]
        public void TestDefaultValues()
        {
            var v = new Vote();
            Assert.AreEqual(v.PraxisId, 0);
            Assert.IsNull(v.Praxis);
            Assert.AreEqual(v.CharacterId, 0);
            Assert.IsNull(v.Character);
            Assert.AreEqual(v.Points, 0);
        }

        [Test]
        public void TestPraxisId()
        {
            Assert.AreEqual(this._praxisId, this._v.PraxisId);
            this._v.PraxisId = 0;
            Assert.AreEqual(0, this._v.PraxisId);
            Assert.Throws<ArgumentException>(()=>this._v.PraxisId = -1);
            Assert.AreEqual(0, this._v.PraxisId);
        }

        [Test]
        public void TestCharacterId()
        {
            Assert.AreEqual(this._characterId, this._v.CharacterId);
            this._v.CharacterId = 0;
            Assert.AreEqual(0, this._v.CharacterId);
            Assert.Throws<ArgumentException>(()=>this._v.CharacterId = -1);
            Assert.AreEqual(0, this._v.CharacterId);
        }

        [Test]
        public void TestPoints()
        {
            Assert.AreEqual(this._points, this._v.Points);

            this._v.Points = 1;
            Assert.AreEqual(1, this._v.Points);

            this._v.Points = 0;
            Assert.AreEqual(0, this._v.Points);

            Assert.Throws<ArgumentException>(()=>this._v.Points = -1);
            Assert.AreEqual(0, this._v.Points);
        }
    }
}