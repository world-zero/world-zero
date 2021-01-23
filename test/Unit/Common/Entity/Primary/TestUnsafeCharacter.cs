using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;
using System;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestCharacter
    {
        private Id _characterId;
        private Id _playerId;
        private Name _name;
        private bool _hasBio;
        private bool _hasProfilePic;
        private PointTotal _eraPoints;
        private PointTotal _totalPoints;
        private PointTotal _votePointsLeft;
        private Id _locationId;
        private Name _factionId;
        private UnsafeCharacter _c;

        [SetUp]
        public void SetUp()
        {
            this._playerId = new Id(5);
            this._characterId = new Id(1);
            this._locationId = new Id(13);
            this._factionId = new Name("Hal's Angels");
            this._name = new Name("Hal's first character.");
            this._hasBio = true;
            this._hasProfilePic = true;
            this._eraPoints = new PointTotal(10);
            this._totalPoints = new PointTotal(20);
            this._votePointsLeft = new PointTotal(75);

            this._c = new UnsafeCharacter(
                this._characterId,
                this._name,
                this._playerId,
                this._factionId,
                this._locationId,
                this._eraPoints,
                this._totalPoints,
                this._votePointsLeft,
                this._hasBio,
                this._hasProfilePic
            );
        }

        [Test]
        public void TestDefaultValues()
        {
            // no char id, faction, location, any points, or bio/pic
            var c = new UnsafeCharacter(
                this._name,
                this._playerId
            );
            Assert.AreEqual(new Id(0), c.Id);
            Assert.IsNull(c.FactionId);
            Assert.IsNull(c.LocationId);
            Assert.AreEqual(new PointTotal(0), c.EraPoints);
            Assert.AreEqual(new PointTotal(0), c.TotalPoints);
            Assert.AreEqual(new PointTotal(100), c.VotePointsLeft);
            Assert.IsFalse(c.HasBio);
            Assert.IsFalse(c.HasProfilePic);
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(this._characterId, this._c.Id);
            Assert.AreEqual(this._characterId, this._c.Id);
            Assert.AreEqual(this._name, this._c.Name);
            Assert.AreEqual(this._playerId, this._c.PlayerId);
            Assert.AreEqual(this._factionId, this._c.FactionId);
            Assert.AreEqual(this._locationId, this._c.LocationId);
            Assert.AreEqual(this._eraPoints, this._c.EraPoints);
            Assert.AreEqual(this._totalPoints, this._c.TotalPoints);
            Assert.AreEqual(this._votePointsLeft, this._c.VotePointsLeft);
            Assert.AreEqual(this._hasBio, this._c.HasBio);
            Assert.AreEqual(this._hasProfilePic, this._c.HasProfilePic);
        }

        [Test]
        public void TestDTOConstructor()
        {
            var c = new UnsafeCharacter(new CharacterDTO(
                this._characterId,
                this._name,
                this._hasBio,
                this._hasProfilePic,
                this._playerId,
                this._votePointsLeft,
                this._eraPoints,
                this._totalPoints,
                new Level(1),
                new Level(1),
                this._factionId,
                this._locationId
            ));
            Assert.AreEqual(this._characterId, c.Id);
            Assert.AreEqual(this._characterId, c.Id);
            Assert.AreEqual(this._name, c.Name);
            Assert.AreEqual(this._playerId, c.PlayerId);
            Assert.AreEqual(this._factionId, c.FactionId);
            Assert.AreEqual(this._locationId, c.LocationId);
            Assert.AreEqual(this._eraPoints, c.EraPoints);
            Assert.AreEqual(this._totalPoints, c.TotalPoints);
            Assert.AreEqual(this._votePointsLeft, c.VotePointsLeft);
            Assert.AreEqual(this._hasBio, c.HasBio);
            Assert.AreEqual(this._hasProfilePic, c.HasProfilePic);
        }

        [Test]
        public void TestPlayerId()
        {
            Assert.AreEqual(this._playerId, this._c.PlayerId);
            this._c.PlayerId = new Id(43);
            Assert.AreEqual(new Id(43), this._c.PlayerId);
            Assert.Throws<ArgumentNullException>(()=>this._c.PlayerId = null);
            Assert.AreEqual(new Id(43), this._c.PlayerId);
        }

        [Test]
        public void TestPoints()
        {
            Assert.AreEqual(this._eraPoints, this._c.EraPoints);
            this._c.EraPoints = new PointTotal(10000);
            Assert.AreEqual(new PointTotal(10000), this._c.EraPoints);
            this._c.EraPoints = null;
            Assert.AreEqual(new PointTotal(0), this._c.EraPoints);

            Assert.AreEqual(this._totalPoints, this._c.TotalPoints);
            this._c.TotalPoints = new PointTotal(4000);
            Assert.AreEqual(new PointTotal(4000), this._c.TotalPoints);
            this._c.TotalPoints = null;
            Assert.AreEqual(new PointTotal(0), this._c.TotalPoints);
        }

        [Test]
        public void TestLocationId()
        {
            Assert.AreEqual(this._locationId, this._c.LocationId);
            this._c.LocationId = new Id(999);
            Assert.AreEqual(new Id(999), this._c.LocationId);
            this._c.LocationId = null;
            Assert.IsNull(this._c.LocationId);
        }

        [Test]
        public void TestFactionId()
        {
            Assert.AreEqual(this._factionId, this._c.FactionId);
            this._c.FactionId = new Name("idk something");
            Assert.AreEqual(new Name("idk something"), this._c.FactionId);
            this._c.FactionId = null;
            Assert.IsNull(this._c.FactionId);
        }
    }
}