using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestEraDTO
    {
        private Name _id;
        private int _maxPraxises;
        private EraDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Name("bar");
            this._maxPraxises = 3;
            this._dto = new EraDTO(this._id, maxPraxises: this._maxPraxises);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.IsNull(this._dto.StartDate);
            Assert.IsNull(this._dto.EndDate);
            Assert.IsNull(this._dto.TaskLevelBuffer);
            Assert.AreEqual(this._maxPraxises, this._dto.MaxPraxises);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as EraDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new EraDTO(this._id, maxPraxises: this._maxPraxises);
            Assert.AreEqual(this._dto, alt);
            alt = new EraDTO(null, maxPraxises: this._maxPraxises);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new EraDTO(this._id, maxPraxises: this._maxPraxises);
            Assert.AreEqual(this._dto, alt);
            alt = new EraDTO(null, maxPraxises: this._maxPraxises);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<EraDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}