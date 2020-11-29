using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestPlayer
    {
        private Id _playerId;
        private Name _name;
        private bool _isBlocked;
        private Player _p;

        [SetUp]
        public void Setup()
        {
            this._playerId = new Id(1);
            this._name = new Name("Jack");
            this._isBlocked = true;

            this._p = new Player(this._playerId, this._name, this._isBlocked);
        }

        [Test]
        public void TestDefaultValues()
        {
            var name = new Name("Hal");
            var p = new Player(name);
            Assert.AreEqual(new Id(0), p.Id);
            Assert.AreEqual(name, p.Name);
            Assert.AreEqual(false, p.IsBlocked);
        }

        [Test]
        public void TestCustomValues()
        {
            Assert.AreEqual(this._playerId, this._p.Id);
            Assert.AreEqual(this._name, this._p.Name);
            Assert.AreEqual(this._isBlocked, this._p.IsBlocked);
        }
    }
}