using WorldZero.Common.Model;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestLocation
    {
        private Location _l;
        private int _locationId;
        private string _city;
        private string _state;
        private string _country;
        private string _zip;

        [SetUp]
        public void Setup()
        {
            this._locationId = 5;
            this._city = "Oregon City";
            this._state = "Oregon";
            this._country = "USA";
            this._zip = "97045";

            this._l = new Location();
            this._l.LocationId = this._locationId;
            this._l.City = this._city;
            this._l.State = this._state;
            this._l.Country = this._country;
            this._l.Zip = this._zip;
        }
        
        [Test]
        public void TestDefaultValues()
        {
            var l = new Location();
            Assert.AreEqual(l.LocationId, 0);
            Assert.IsNull(l.City);
            Assert.IsNull(l.State);
            Assert.IsNull(l.Country);
            Assert.IsNull(l.Zip);
        }

        [Test]
        public void TestLocationId()
        {
            Assert.AreEqual(this._locationId, this._l.LocationId);
            this._l.LocationId = 0;
            Assert.AreEqual(0, this._l.LocationId);
            Assert.Throws<ArgumentException>(()=>this._l.LocationId = -1);
            Assert.AreEqual(0, this._l.LocationId);
        }

        [Test]
        public void TestCity()
        {
            Assert.AreEqual(this._city, this._l.City);
            this._l.City = "Test";
            Assert.AreEqual("Test", this._l.City);
            Assert.Throws<ArgumentException>(()=>this._l.City = null);
            Assert.Throws<ArgumentException>(()=>this._l.City = "");
            Assert.Throws<ArgumentException>(()=>this._l.City = "     ");
            Assert.AreEqual("Test", this._l.City);
        }

        [Test]
        public void TestState()
        {
            Assert.AreEqual(this._state, this._l.State);
            this._l.State = "Test";
            Assert.AreEqual("Test", this._l.State);
            Assert.Throws<ArgumentException>(()=>this._l.State = null);
            Assert.Throws<ArgumentException>(()=>this._l.State = "");
            Assert.Throws<ArgumentException>(()=>this._l.State = "     ");
            Assert.AreEqual("Test", this._l.State);
        }

        [Test]
        public void TestCountry()
        {
            Assert.AreEqual(this._country, this._l.Country);
            this._l.Country = "Test";
            Assert.AreEqual("Test", this._l.Country);
            Assert.Throws<ArgumentException>(()=>this._l.Country = null);
            Assert.Throws<ArgumentException>(()=>this._l.Country = "");
            Assert.Throws<ArgumentException>(()=>this._l.Country = "     ");
            Assert.AreEqual("Test", this._l.Country);
        }

        [Test]
        public void TestZip()
        {
            Assert.AreEqual(this._zip, this._l.Zip);
            this._l.Zip = "Test";
            Assert.AreEqual("Test", this._l.Zip);
            Assert.Throws<ArgumentException>(()=>this._l.Zip = null);
            Assert.Throws<ArgumentException>(()=>this._l.Zip = "");
            Assert.Throws<ArgumentException>(()=>this._l.Zip = "     ");
            Assert.AreEqual("Test", this._l.Zip);
        }
    }
}