using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM.Generic.Primary
{
    [TestFixture]
    public class TestIRAMIdNamedEntityRepo
    {
        private Name _name;
        private UnsafePlayer _player;
        private TestRAMIdNamedEntityRepo _repo;

        private void _assertPlayersEqual(UnsafePlayer expected, UnsafePlayer actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsBlocked, actual.IsBlocked);
        }

        [SetUp]
        public void Setup()
        {
            this._name = new Name("Hal");
            this._player = new UnsafePlayer(this._name);
            this._repo = new TestRAMIdNamedEntityRepo();
            this._repo.Insert(this._player);
            this._repo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._repo.CleanAll();
            this._repo.ResetNextIdValue();
        }

        [Test]
        public void TestGetByName()
        {
            Assert.Throws<ArgumentNullException>(()=>this._repo.GetByName(null));
            Assert.Throws<ArgumentException>(()=>this._repo.GetByName(new Name("fake")));

            UnsafePlayer actual = this._repo.GetByName(this._player.Name);
            Assert.AreEqual(this._player.Name, actual.Name);
            Assert.AreEqual(this._player.Id, actual.Id);
        }
    }

    public class TestRAMIdNamedEntityRepo
        : IRAMIdNamedEntityRepo<UnsafePlayer>
    {
        public TestRAMIdNamedEntityRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }

        protected override int GetRuleCount()
        {
            var a = new UnsafePlayer(new Name("Jack"));
            return a.GetUniqueRules().Count;
        }
    }
}