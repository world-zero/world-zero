using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestPlayer
    {
        private int _playerId;
        private string _username;
        private bool _isBlocked;
        private Player _p;

        [SetUp]
        public void Setup()
        {
            this._playerId = 1;
            this._username = "Jack";
            this._isBlocked = false;

            this._p = new Player();
            this._p.PlayerId = this._playerId;
            this._p.Username = this._username;
            this._p.IsBlocked = this._isBlocked;
        }

        [Test]
        public void TestDefaultValues()
        {
            var p = new Player();
            Assert.AreEqual(p.PlayerId, 0);
            Assert.IsNull(p.Username);
            Assert.AreEqual(p.IsBlocked, false);
        }

        [Test]
        public void TestPlayerId()
        {
            Assert.AreEqual(this._playerId, this._p.PlayerId);
            this._p.PlayerId = 0;
            Assert.AreEqual(0, this._p.PlayerId);
            Assert.Throws<ArgumentException>(()=>this._p.PlayerId = -1);
            Assert.AreEqual(0, this._p.PlayerId);
        }

        [Test]
        public void TestUsername()
        {
            Assert.AreEqual(this._p.Username, this._username);
            this._p.Username = null;
            Assert.AreEqual(this._p.Username, null);

            // 25 and 26 character strings, respectively.
            this._p.Username =         "asdfjkl;qweryuiopertygndk";
            Assert.Throws<ArgumentException>(
                ()=>this._p.Username = "asdfjkl;qweryuiopertygndkX");
        }

        [Test]
        public void TestIsBlocked()
        {
            Assert.AreEqual(this._p.IsBlocked, this._isBlocked);
            this._p.IsBlocked = true;
            Assert.AreEqual(this._p.IsBlocked, true);
            this._p.IsBlocked = false;
            Assert.AreEqual(this._p.IsBlocked, false);
        }
    }
}