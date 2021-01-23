using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestUnsafeLocation
    {
        private UnsafeLocation _l;
        private Id _id;
        private Name _city;
        private Name _state;
        private Name _country;
        private Name _zip;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._city = new Name("Oregon City");
            this._state = new Name("Oregon");
            this._country = new Name("USA");
            this._zip = new Name("97045");

            this._l = new UnsafeLocation(
                this._id,
                this._city,
                this._state,
                this._country,
                this._zip
            );
        }
        
        [Test]
        public void TestDefaultValues()
        {
            var l = new UnsafeLocation(
                this._city,
                this._state,
                this._country,
                this._zip
            );
            Assert.AreEqual(new Id(0), l.Id);
        }

        [Test]
        public void TestDTOConstructor()
        {
            var l = new UnsafeLocation(new LocationDTO(
                this._id,
                this._city,
                this._state,
                this._country,
                this._zip
            ));
            Assert.AreEqual(this._id, l.Id);
            Assert.AreEqual(this._city, l.City);
            Assert.AreEqual(this._state, l.State);
            Assert.AreEqual(this._country, l.Country);
            Assert.AreEqual(this._zip, l.Zip);
        }

        [Test]
        public void TestCity()
        {
            Assert.AreEqual(this._city, this._l.City);
            this._l.City = new Name("Test");
            Assert.AreEqual(new Name("Test"), this._l.City);
            Assert.Throws<ArgumentNullException>(()=>this._l.City = null);
            Assert.AreEqual(new Name("Test"), this._l.City);
        }

        [Test]
        public void TestState()
        {
            Assert.AreEqual(this._state, this._l.State);
            this._l.State = new Name("Test");
            Assert.AreEqual(new Name("Test"), this._l.State);
            Assert.Throws<ArgumentNullException>(()=>this._l.State = null);
            Assert.AreEqual(new Name("Test"), this._l.State);
        }

        [Test]
        public void TestCountry()
        {
            Assert.AreEqual(this._country, this._l.Country);
            this._l.Country = new Name("Test");
            Assert.AreEqual(new Name("Test"), this._l.Country);
            Assert.Throws<ArgumentNullException>(()=>this._l.Country = null);
            Assert.AreEqual(new Name("Test"), this._l.Country);
        }

        [Test]
        public void TestZip()
        {
            Assert.AreEqual(this._zip, this._l.Zip);
            this._l.Zip = new Name("Test");
            Assert.AreEqual(new Name("Test"), this._l.Zip);
            Assert.Throws<ArgumentNullException>(()=>this._l.Zip = null);
            Assert.AreEqual(new Name("Test"), this._l.Zip);
        }
    }
}