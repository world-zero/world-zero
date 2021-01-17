using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestLocationDTO
    {
        private Id _id;
        private Name _city;
        private LocationDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._city = new Name("foo");
            this._dto = new LocationDTO(this._id, this._city);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._city, this._dto.City);
            Assert.IsNull(this._dto.State);
            Assert.IsNull(this._dto.Country);
            Assert.IsNull(this._dto.Zip);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as LocationDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new LocationDTO(this._id, this._city);
            Assert.AreEqual(this._dto, alt);
            alt = new LocationDTO(null, this._city);
            Assert.AreNotEqual(this._dto, alt);
            alt = new LocationDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new LocationDTO(this._id, this._city);
            Assert.AreEqual(this._dto, alt);
            alt = new LocationDTO(null, this._city);
            Assert.AreNotEqual(this._dto, alt);
            alt = new LocationDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<LocationDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}