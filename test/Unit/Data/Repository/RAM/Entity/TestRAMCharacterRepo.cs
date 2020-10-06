using System;
using System.Linq;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity
{
    [TestFixture]
    public class TestRAMCharacterRepo
    {
        private void _assertCharsEqual(Character expected, Character actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FactionId, actual.FactionId);
            Assert.AreEqual(expected.LocationId, actual.LocationId);
            Assert.AreEqual(expected.EraPoints, actual.EraPoints);
            Assert.AreEqual(expected.EraLevel, actual.EraLevel);
            Assert.AreEqual(expected.TotalPoints, actual.TotalPoints);
            Assert.AreEqual(expected.TotalLevel, actual.TotalLevel);
            Assert.AreEqual(expected.VotePointsLeft, actual.VotePointsLeft);
            Assert.AreEqual(expected.HasBio, actual.HasBio);
            Assert.AreEqual(expected.HasProfilePic, actual.HasProfilePic);
        }

        private RAMCharacterRepo _charRepo;

        [SetUp]
        public void Setup()
        {
            this._charRepo = new RAMCharacterRepo();
        }

        [Test]
        public void TestGetByPlayerId()
        {
            Assert.Throws<ArgumentException>(()=>
                this._charRepo.GetByPlayerId(new Id(234)));

            var c0 = new Character(new Name("DIO"), new Id(1));
            var c1 = new Character(new Name("Joturo Kujo"), new Id(2));
            var c2 = new Character(new Name("Iggy"), new Id(2));

            this._charRepo.Insert(c0);
            this._charRepo.Insert(c1);
            this._charRepo.Insert(c2);
            this._charRepo.Save();

            var actualId1 = this._charRepo.GetByPlayerId(c0.PlayerId);
            Assert.AreEqual(1, actualId1.Count());
            this._assertCharsEqual(c0, actualId1.First());

            Assert.AreEqual(c1.PlayerId, c2.PlayerId);
            var actualId2 = this._charRepo.GetByPlayerId(c1.PlayerId).ToList();
            Assert.AreEqual(2, actualId2.Count);
            this._assertCharsEqual(c1, actualId2[0]);
            this._assertCharsEqual(c2, actualId2[1]);
        }
    }
}