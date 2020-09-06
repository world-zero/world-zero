using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestFaction
    {
        private Name _name;
        private PastDate _dateFounded;
        private string _desc;
        private Name _abilityName;
        private Faction _f;

        [SetUp]
        public void Setup()
        {
            this._name = new Name("The Jo-Bros");
            this._dateFounded = new PastDate(DateTime.UtcNow);
            this._desc = "primary description";
            this._abilityName = new Name("Plot Power");

            this._f = new Faction(this._name, this._dateFounded, this._desc, this._abilityName);
        }

        [Test]
        public void TestDefaultValues()
        {
            var f = new Faction(this._name, new PastDate(DateTime.UtcNow));
            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                f.DateFounded.Get.ToString("MM:dd:yyyy HH")
            );
            Assert.IsNull(f.Description);
            Assert.IsNull(f.AbilityName);
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(
                DateTime.UtcNow.ToString("MM:dd:yyyy HH"),
                this._f.DateFounded.Get.ToString("MM:dd:yyyy HH")
            );
            Assert.AreEqual(this._desc, this._f.Description);
            Assert.AreEqual(this._abilityName, this._f.AbilityName);
        }

        [Test]
        public void TestDateFounded()
        {
            Assert.AreEqual(this._dateFounded, this._f.DateFounded);
            var newDT = new PastDate(DateTime.UtcNow);
            this._f.DateFounded = newDT;
            Assert.AreEqual(this._f.DateFounded, newDT);
            Assert.Throws<ArgumentNullException>(()=>this._f.DateFounded = null);
            Assert.Throws<ArgumentException>(()=>this._f.DateFounded = new PastDate(new DateTime(3000, 5, 1, 8, 30, 52)));
            Assert.AreEqual(this._f.DateFounded, newDT);
        }
    }
}