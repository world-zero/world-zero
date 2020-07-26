using WorldZero.Common.Entity;
using NUnit.Framework;
using System;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestCharacter
    {
        private int _playerId;
        private Character _c;
        private int _characterId;
        private string _Name;
        private bool _hasBio;
        private bool _hasProfilePic;
        private int _eraPoints;
        private int _totalPoints;
        private int _votePointsLeft;
        private int _locationId;
        private string _factionId;

        [SetUp]
        public void SetUp()
        {
            this._playerId = 5;
            this._characterId = 1;
            this._Name = "Hal's first character.";
            this._hasBio = false;
            this._hasProfilePic = false;
            this._eraPoints = 10;
            this._totalPoints = 20;
            // Level members are omitted because they are not stored, only
            // calculated on request.
            this._votePointsLeft = 100;
            this._locationId = 30;
            this._factionId = "Hal's Angels";

            this._c = new Character();
            this._c.PlayerId = this._playerId;
            this._c.Id = this._characterId;
            this._c.Name = this._Name;
            this._c.HasBio = this._hasBio;
            this._c.HasProfilePic = this._hasProfilePic;
            this._c.EraPoints = this._eraPoints;
            this._c.TotalPoints = this._totalPoints;
            this._c.VotePointsLeft = this._votePointsLeft;
            this._c.LocationId = this._locationId;
            this._c.FactionId = this._factionId;
        }

        [Test]
        public void TestDefaultValues()
        {
            var c = new Character();
            Assert.AreEqual(c.Id, 0);
            Assert.AreEqual(c.PlayerId, 0);
            Assert.IsNull(c.Player);
            Assert.IsNull(c.Name);
            Assert.IsFalse(c.HasBio);
            Assert.IsFalse(c.HasProfilePic);
            Assert.AreEqual(c.EraPoints, 0);
            Assert.AreEqual(c.TotalPoints, 0);
            Assert.AreEqual(c.EraLevel, 0);
            Assert.AreEqual(c.TotalLevel, 0);
            Assert.AreEqual(c.VotePointsLeft, 100);
            Assert.IsNull(c.Faction);
            Assert.IsNull(c.FactionId);
            Assert.IsNull(c.Location);
            Assert.IsNull(c.LocationId);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._characterId, this._c.Id);
            this._c.Id = 0;
            Assert.AreEqual(0, this._c.Id);
            Assert.Throws<ArgumentException>(()=>this._c.Id = -1);
            Assert.AreEqual(0, this._c.Id);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._Name, this._c.Name);
            this._c.Name = "Test";
            Assert.AreEqual("Test", this._c.Name);
            Assert.Throws<ArgumentException>(()=>this._c.Name = null);
            Assert.Throws<ArgumentException>(()=>this._c.Name = "");
            Assert.Throws<ArgumentException>(()=>this._c.Name = "     ");
            Assert.AreEqual("Test", this._c.Name);
        }

        [Test]
        public void TestPlayerId()
        {
            Assert.AreEqual(this._playerId, this._c.PlayerId);
            this._c.PlayerId = 0;
            Assert.AreEqual(0, this._c.PlayerId);
            Assert.Throws<ArgumentException>(()=>this._c.PlayerId = -1);
            Assert.AreEqual(0, this._c.PlayerId);
        }

        [Test]
        public void TestPoints()
        {
            Assert.AreEqual(this._eraPoints, this._c.EraPoints);
            this._c.EraPoints = 0;
            Assert.AreEqual(0, this._c.EraPoints);
            Assert.Throws<ArgumentException>(()=>this._c.EraPoints = -1);
            Assert.AreEqual(0, this._c.EraPoints);

            Assert.AreEqual(this._totalPoints, this._c.TotalPoints);
            this._c.TotalPoints = 0;
            Assert.AreEqual(0, this._c.TotalPoints);
            Assert.Throws<ArgumentException>(()=>this._c.TotalPoints = -1);
            Assert.AreEqual(0, this._c.TotalPoints);
        }

        [Test]
        public void TestEraLevel()
        {
            this._c.EraPoints = 0;
            Assert.AreEqual(this._c.EraLevel, 0);
            this._c.EraPoints = 10-1;
            Assert.AreEqual(this._c.EraLevel, 0);

            this._c.EraPoints = 10;
            Assert.AreEqual(this._c.EraLevel, 1);
            this._c.EraPoints = 70-1;
            Assert.AreEqual(this._c.EraLevel, 1);

            this._c.EraPoints = 70;
            Assert.AreEqual(this._c.EraLevel, 2);
            this._c.EraPoints = 170-1;
            Assert.AreEqual(this._c.EraLevel, 2);

            this._c.EraPoints = 170;
            Assert.AreEqual(this._c.EraLevel, 3);
            this._c.EraPoints = 330-1;
            Assert.AreEqual(this._c.EraLevel, 3);

            this._c.EraPoints = 330;
            Assert.AreEqual(this._c.EraLevel, 4);
            this._c.EraPoints = 610-1;
            Assert.AreEqual(this._c.EraLevel, 4);

            this._c.EraPoints = 610;
            Assert.AreEqual(this._c.EraLevel, 5);
            this._c.EraPoints = 1090-1;
            Assert.AreEqual(this._c.EraLevel, 5);

            this._c.EraPoints = 1090;
            Assert.AreEqual(this._c.EraLevel, 6);
            this._c.EraPoints = 1840-1;
            Assert.AreEqual(this._c.EraLevel, 6);

            this._c.EraPoints = 1840;
            Assert.AreEqual(this._c.EraLevel, 7);
            this._c.EraPoints = 3040-1;
            Assert.AreEqual(this._c.EraLevel, 7);

            this._c.EraPoints = 3040;
            Assert.AreEqual(this._c.EraLevel, 8);
            this._c.EraPoints = 3041;
            Assert.AreEqual(this._c.EraLevel, 8);
            this._c.EraPoints = 1000000;
            Assert.AreEqual(this._c.EraLevel, 8);
        }

        [Test]
        public void TestTotalLevel()
        {
            this._c.TotalPoints = 0;
            Assert.AreEqual(this._c.TotalLevel, 0);
            this._c.TotalPoints = 10-1;
            Assert.AreEqual(this._c.TotalLevel, 0);

            this._c.TotalPoints = 10;
            Assert.AreEqual(this._c.TotalLevel, 1);
            this._c.TotalPoints = 70-1;
            Assert.AreEqual(this._c.TotalLevel, 1);

            this._c.TotalPoints = 70;
            Assert.AreEqual(this._c.TotalLevel, 2);
            this._c.TotalPoints = 170-1;
            Assert.AreEqual(this._c.TotalLevel, 2);

            this._c.TotalPoints = 170;
            Assert.AreEqual(this._c.TotalLevel, 3);
            this._c.TotalPoints = 330-1;
            Assert.AreEqual(this._c.TotalLevel, 3);

            this._c.TotalPoints = 330;
            Assert.AreEqual(this._c.TotalLevel, 4);
            this._c.TotalPoints = 610-1;
            Assert.AreEqual(this._c.TotalLevel, 4);

            this._c.TotalPoints = 610;
            Assert.AreEqual(this._c.TotalLevel, 5);
            this._c.TotalPoints = 1090-1;
            Assert.AreEqual(this._c.TotalLevel, 5);

            this._c.TotalPoints = 1090;
            Assert.AreEqual(this._c.TotalLevel, 6);
            this._c.TotalPoints = 1840-1;
            Assert.AreEqual(this._c.TotalLevel, 6);

            this._c.TotalPoints = 1840;
            Assert.AreEqual(this._c.TotalLevel, 7);
            this._c.TotalPoints = 3040-1;
            Assert.AreEqual(this._c.TotalLevel, 7);

            this._c.TotalPoints = 3040;
            Assert.AreEqual(this._c.TotalLevel, 8);
            this._c.TotalPoints = 3041;
            Assert.AreEqual(this._c.TotalLevel, 8);
            this._c.TotalPoints = 1000000;
            Assert.AreEqual(this._c.TotalLevel, 8);
        }

        [Test]
        public void TestLocationId()
        {
            Assert.AreEqual(this._locationId, this._c.LocationId);
            this._c.LocationId = 0;
            Assert.AreEqual(0, this._c.LocationId);
            Assert.Throws<ArgumentException>(()=>this._c.LocationId = -1);
            Assert.AreEqual(0, this._c.LocationId);

            this._c.LocationId = null;
            Assert.IsNull(this._c.LocationId);
        }

        [Test]
        public void TestFactionId()
        {
            Assert.AreEqual(this._factionId, this._c.FactionId);
            this._c.FactionId = "Jack's Jackles";
            Assert.AreEqual("Jack's Jackles", this._c.FactionId);
            Assert.Throws<ArgumentException>(()=>this._c.FactionId = "");
            Assert.Throws<ArgumentException>(()=>this._c.FactionId = "   ");
            Assert.AreEqual("Jack's Jackles", this._c.FactionId);

            this._c.FactionId = null;
            Assert.IsNull(this._c.FactionId);
        }
    }
}