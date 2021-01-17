using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestCharacterDTO
    {
        private Id _id;
        private Name _factionId;
        private CharacterDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._factionId = new Name("DIO");
            this._dto = new CharacterDTO(this._id, factionId: this._factionId);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.IsNull(this._dto.Name);
            Assert.IsFalse(this._dto.HasBio);
            Assert.IsFalse(this._dto.HasProfilePic);
            Assert.IsNull(this._dto.PlayerId);
            Assert.IsNull(this._dto.VotePointsLeft);
            Assert.IsNull(this._dto.EraPoints);
            Assert.IsNull(this._dto.TotalPoints);
            Assert.IsNull(this._dto.EraLevel);
            Assert.IsNull(this._dto.TotalLevel);
            Assert.AreEqual(this._factionId, this._dto.FactionId);
            Assert.IsNull(this._dto.LocationId);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as CharacterDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new CharacterDTO(this._id, factionId: this._factionId);
            Assert.AreEqual(this._dto, alt);
            alt = new CharacterDTO(null, factionId: this._factionId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new CharacterDTO(this._id, factionId: null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new CharacterDTO(this._id, factionId: this._factionId);
            Assert.AreEqual(this._dto, alt);
            alt = new CharacterDTO(null, factionId: this._factionId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new CharacterDTO(this._id, factionId: null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<CharacterDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}