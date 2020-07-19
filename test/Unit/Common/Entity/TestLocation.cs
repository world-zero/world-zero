using WorldZero.Common.Entity;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestLocation
    {
        private Location _l;
        private int _id;
        private string _city;
        private string _state;
        private string _country;
        private string _zip;

        [SetUp]
        public void Setup()
        {
            this._id = 5;
            this._city = "Oregon City";
            this._state = "Oregon";
            this._country = "USA";
            this._zip = "97045";

            this._l = new Location();
            this._l.Id = this._id;
            this._l.City = this._city;
            this._l.State = this._state;
            this._l.Country = this._country;
            this._l.Zip = this._zip;
        }
        
        [Test]
        public void TestDefaultValues()
        {
            var l = new Location();
            Assert.AreEqual(l.Id, 0);
            Assert.IsNull(l.City);
            Assert.IsNull(l.State);
            Assert.IsNull(l.Country);
            Assert.IsNull(l.Zip);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._id, this._l.Id);
            this._l.Id = 0;
            Assert.AreEqual(0, this._l.Id);
            Assert.Throws<ArgumentException>(()=>this._l.Id = -1);
            Assert.AreEqual(0, this._l.Id);
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