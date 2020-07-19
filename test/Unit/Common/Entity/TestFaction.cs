using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestFaction
    {
        private Faction _f;
        private string _name;
        private string _abilityName;
        private DateTime _dateFounded;

        [SetUp]
        public void Setup()
        {
            this._name = "The Jo-Bros";
            this._abilityName = "Plot Power";
            this._dateFounded = DateTime.UtcNow;

            this._f = new Faction();
            this._f.Name = this._name;
            this._f.AbilityName = this._abilityName;
            this._f.DateFounded = this._dateFounded;
        }

        [Test]
        public void TestDefaultValues()
        {
            var f = new Faction();
            // Ignore the minutes/milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(f.DateFounded.ToString("MM:dd:yyyy HH"), DateTime.UtcNow.ToString("MM:dd:yyyy HH"));
            Assert.IsNull(f.Name);
            Assert.IsNull(f.Description);
            Assert.IsNull(f.AbilityName);
            Assert.IsNull(f.AbilityDesc);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._name, this._f.Name);
            this._f.Name = "DIO";
            Assert.AreEqual("DIO", this._f.Name);
            Assert.Throws<ArgumentException>(()=>this._f.Name = null);
            Assert.Throws<ArgumentException>(()=>this._f.Name = "");
            Assert.Throws<ArgumentException>(()=>this._f.Name = "   ");
            Assert.AreEqual("DIO", this._f.Name);
        }

        [Test]
        public void TestAbilityName()
        {
            Assert.AreEqual(this._abilityName, this._f.AbilityName);
            this._f.AbilityName = "The World";
            Assert.AreEqual("The World", this._f.AbilityName);
            Assert.Throws<ArgumentException>(()=>this._f.AbilityName = null);
            Assert.Throws<ArgumentException>(()=>this._f.AbilityName = "");
            Assert.Throws<ArgumentException>(()=>this._f.AbilityName = "   ");
            Assert.AreEqual("The World", this._f.AbilityName);
        }

        [Test]
        public void TestDateFounded()
        {
            Assert.AreEqual(this._dateFounded, this._f.DateFounded);
            var newDT = DateTime.UtcNow;
            this._f.DateFounded = newDT;
            Assert.AreEqual(this._f.DateFounded, newDT);
            Assert.Throws<ArgumentException>(()=>this._f.DateFounded = new DateTime(3000, 5, 1, 8, 30, 52));
            Assert.AreEqual(this._f.DateFounded, newDT);
        }
    }
}