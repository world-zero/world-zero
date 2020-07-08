using WorldZero.Domain.Entity;
using WorldZero.Domain.Model;
using NUnit.Framework;
using System;

namespace WorldZero.Test.Unit.Domain.ValueObject
{
    [TestFixture]
    public class TestPlayer
    {
        private Player _p;
        private string _username = "Hal";
        private bool _isBlocked = false;

        [SetUp]
        public void CreateInstance()
        {
            this._p = new Player(this._username, this._isBlocked);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._p.Username == this._username);
            Assert.IsTrue(this._p.IsBlocked == this._isBlocked);
            Assert.NotNull(this._p.Model);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new Player(null, false));
            Assert.Throws<ArgumentException>(()=>new Player("", false));
            Assert.Throws<ArgumentException>(()=>new Player("   ", false));
        }

        [Test]
        public void TestConstructorModel()
        {
            var happy = new PlayerModel()
            {
                PlayerId = 0,
                Username = "Makenna",
                IsBlocked = false
            };
            var p = new Player(happy);
            Assert.IsTrue(p.PlayerId == 0);
            Assert.IsTrue(p.Username == "Makenna");
            Assert.IsFalse(p.IsBlocked);
            Assert.NotNull(p.Model);
            Assert.NotNull(p.Model.Characters);

            var bad = new PlayerModel()
            {
                PlayerId = -1,
                Username = "Makenna",
                IsBlocked = false
            };
            Assert.Throws<ArgumentException>(()=>p = new Player(bad));
            bad = new PlayerModel()
            {
                PlayerId = 0,
                Username = null,
                IsBlocked = false
            };
            Assert.Throws<ArgumentException>(()=>p = new Player(bad));
            bad = new PlayerModel()
            {
                PlayerId = 0,
                Username = "",
                IsBlocked = false
            };
            Assert.Throws<ArgumentException>(()=>p = new Player(bad));
            bad = new PlayerModel()
            {
                PlayerId = 0,
                Username = "      ",
                IsBlocked = false
            };
            Assert.Throws<ArgumentException>(()=>p = new Player(bad));
        }

        [Test]
        public void TestProperties()
        {
            this._p.Username = "Jack";
            Assert.IsTrue(this._p.Username == "Jack");
            Assert.Throws<ArgumentException>(()=>this._p.Username = null);
            Assert.Throws<ArgumentException>(()=>this._p.Username = "");
            Assert.Throws<ArgumentException>(()=>this._p.Username = "     ");

            this._p.IsBlocked = true;
            Assert.IsTrue(this._p.IsBlocked);

            this._p.PlayerId = 0;
            Assert.IsTrue(this._p.PlayerId == 0);
            this._p.PlayerId = 1;
            Assert.IsTrue(this._p.PlayerId == 1);
            Assert.Throws<ArgumentException>(()=>this._p.PlayerId = -1);
        }
    }
}