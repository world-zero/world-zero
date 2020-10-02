using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity
{
    [TestFixture]
    public class TestIRAMIdNamedEntityRepo
    {
        private Name _name;
        private Player _player;
        private TestRAMIdNamedEntityRepo _repo;

        private void _assertPlayersEqual(Player expected, Player actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsBlocked, actual.IsBlocked);
        }

        [SetUp]
        public void Setup()
        {
            this._name = new Name("Hal");
            this._player = new Player(this._name);
            this._repo = new TestRAMIdNamedEntityRepo();
            this._repo.Insert(this._player);
            this._repo.Save();
        }

        [Test]
        public void TestGetByName()
        {
            Assert.Throws<ArgumentNullException>(()=>this._repo.GetByName(null));
            Assert.Throws<ArgumentException>(()=>this._repo.GetByName(new Name("fake")));

            Player actual = this._repo.GetByName(this._player.Name);
            Assert.AreEqual(this._player.Name, actual.Name);
            Assert.AreEqual(this._player.Id, actual.Id);
        }
    }

    public class TestRAMIdNamedEntityRepo
        : IRAMIdNamedEntityRepo<Player>
    {
        public TestRAMIdNamedEntityRepo()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new Player(new Name("Jack"));
            return a.GetUniqueRules().Count;
        }
    }
}