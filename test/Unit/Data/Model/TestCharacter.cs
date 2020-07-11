using WorldZero.Data.Model;
using NUnit.Framework;
using System;

namespace WorldZero.Test.Unit.Data.Model
{
    /*
    [TestFixture]
    public class TestCharacter
    {
        private string _username;
        private bool _isBlocked;
        private Player _p;
        private string _displayname;
        private bool _hasBio;
        private bool _hasProfilePic;
        private int _eraPoints;
        private int _totalPoints;
        private int _eraLevel;
        private int _totalLevel;
        private int _votePointsLeft;
        private Character _c;

        [SetUp]
        public void CreateInstance()
        {
            this._username = "Hal";
            this._isBlocked = false;
            this._p = new Player(this._username, this._isBlocked);
            this._displayname = "Hal's first character.";
            this._hasBio = false;
            this._eraPoints = 0;
            this._totalPoints = 0;
            this._eraLevel = 0;
            this._totalLevel = 0;
            this._votePointsLeft = 100;
            this._c = new Character(
                this._p,
                this._displayname,
                this._hasBio,
                this._hasProfilePic,
                this._eraPoints,
                this._totalPoints,
                this._eraLevel,
                this._totalLevel,
                this._votePointsLeft);
        }

        // TODO: update all tests after this line.
        // constructor tests: fewest args, max args, varyingly invalid model

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
        }
    }
    //*/
}