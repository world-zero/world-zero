using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestComment
    {
        private Comment _c;
        private DateTime _date;
        private int _praxisId;
        private int _characterId;

        [SetUp]
        public void Setup()
        {
            this._praxisId = 5;
            this._characterId = 10;
            this._date = DateTime.UtcNow;

            this._c = new Comment();
            this._c.PraxisId = this._praxisId;
            this._c.CharacterId = this._characterId;
            this._c.DateCreated = this._date;
        }

        [Test]
        public void TestDefaultValues()
        {
            var c = new Comment();
            Assert.AreEqual(c.PraxisId, 0);
            Assert.IsNull(c.Praxis);
            Assert.AreEqual(c.CharacterId, 0);
            Assert.IsNull(c.Character);
            Assert.IsNull(c.Value);
            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(c.DateCreated.ToString("MM:dd:yyyy HH"), DateTime.UtcNow.ToString("MM:dd:yyyy HH"));
        }

        [Test]
        public void TestPraxisId()
        {
            Assert.AreEqual(this._praxisId, this._c.PraxisId);
            this._c.PraxisId = 0;
            Assert.AreEqual(0, this._c.PraxisId);
            Assert.Throws<ArgumentException>(()=>this._c.PraxisId = -1);
            Assert.AreEqual(0, this._c.PraxisId);
        }

        [Test]
        public void TestCharacterId()
        {
            Assert.AreEqual(this._characterId, this._c.CharacterId);
            this._c.CharacterId = 0;
            Assert.AreEqual(0, this._c.CharacterId);
            Assert.Throws<ArgumentException>(()=>this._c.CharacterId = -1);
            Assert.AreEqual(0, this._c.CharacterId);
        }

        [Test]
        public void TestDateFounded()
        {
            var d = DateTime.UtcNow;
            this._c.DateCreated = d;
            Assert.AreEqual(this._c.DateCreated, d);
            Assert.Throws<ArgumentException>(()=>this._c.DateCreated = new DateTime(3000, 5, 1, 8, 30, 52));
            Assert.AreEqual(this._c.DateCreated, d);
        }
    }
}