using System;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestPlayer
    {
        private int _playerId;
        private string _Name;
        private bool _isBlocked;
        private Player _p;

        [SetUp]
        public void Setup()
        {
            this._playerId = 1;
            this._Name = "Jack";
            this._isBlocked = false;

            this._p = new Player();
            this._p.Id = this._playerId;
            this._p.Name = this._Name;
            this._p.IsBlocked = this._isBlocked;
        }

        [Test]
        public void TestDefaultValues()
        {
            var p = new Player();
            Assert.AreEqual(p.Id, 0);
            Assert.IsNull(p.Name);
            Assert.AreEqual(p.IsBlocked, false);
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._playerId, this._p.Id);
            this._p.Id = 0;
            Assert.AreEqual(0, this._p.Id);
            Assert.Throws<ArgumentException>(()=>this._p.Id = -1);
            Assert.AreEqual(0, this._p.Id);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(this._p.Name, this._Name);
            Assert.Throws<ArgumentException>(()=>this._p.Name = null);
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