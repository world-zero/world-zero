using System;
using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM
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
            Assert.Throws<ArgumentException>(()=>this._repo.GetByName(null));
            Assert.Throws<ArgumentException>(()=>this._repo.GetByName(new Name("fake")));

            Player actual = this._repo.GetByName(this._player.Name);
            Assert.AreEqual(this._player.Name, actual.Name);
            Assert.AreEqual(this._player.Id, actual.Id);
        }

        [Test]
        public void TestInsertSave()
        {
            var repo = new TestRAMIdNamedEntityRepo();

            Assert.AreEqual(0, repo.SavedNames.Count);
            Assert.AreEqual(0, repo.StagedNames.Count);

            var name = new Name("something");
            var p = new Player(name);
            repo.Insert(p);
            Assert.AreEqual(0, repo.SavedNames.Count);
            Assert.AreEqual(1, repo.StagedNames.Count);
            Assert.IsFalse(repo.SavedNames.ContainsKey(name));
            Assert.IsTrue(repo.StagedNames.ContainsKey(name));
            Assert.IsFalse(p.IsIdSet());

            repo.Save();
            Assert.AreEqual(1, repo.SavedNames.Count);
            Assert.AreEqual(0, repo.StagedNames.Count);
            Assert.IsTrue(repo.SavedNames.ContainsKey(name));
            Assert.IsFalse(repo.StagedNames.ContainsKey(name));

            var newName = new Name("School of Rock");
            repo.Insert(new Player(newName));
            Assert.Throws<ArgumentException>(()=>repo.Insert(new Player(newName)));
            repo.Save();
            Assert.AreEqual(2, repo.SavedNames.Count);
            Assert.AreEqual(0, repo.StagedNames.Count);
        }

        [Test]
        public void TestInsertBad()
        {
            var newName = new Name("School of Rock");
            Assert.Throws<ArgumentException>(
                ()=>this._repo.Insert(new Player(new Id(9), newName)));

            this._repo.Insert(new Player(newName));
            Assert.Throws<ArgumentException>(()=>this._repo.Insert(new Player(newName)));
        }

        [Test]
        public void TestDeleteInsertFreedName()
        {
            this._repo.Delete(this._player.Id);
            Assert.AreEqual(0, this._repo.RecycledNames.Count);
            var p = new Player(this._name);
            this._repo.Insert(p);
            Assert.AreEqual(1, this._repo.RecycledNames.Count);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.RecycledNames.Count);
            this._assertPlayersEqual(p, this._repo.SavedNames[p.Name]);
        }

        [Test]
        public void TestUpdateNonNameChange()
        {
            this._player.IsBlocked = true;

            this._repo.Update(this._player);
            Assert.AreEqual(1, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);

            this._repo.Save();
            Assert.AreEqual(1, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);
        }

        [Test]
        public void TestUpdateNameChange()
        {
            var newName = new Name("Jack Black");
            this._player.Name = newName;
            this._repo.Update(this._player);
            Assert.AreEqual(1, this._repo.SavedNames.Count);
            Assert.AreEqual(2, this._repo.StagedNames.Count);
            Assert.IsTrue(this._repo.SavedNames.ContainsKey(this._name));
            Assert.IsTrue(this._repo.StagedNames.ContainsKey(newName));
            Assert.IsNull(this._repo.StagedNames[this._name]);
            Assert.AreEqual(this._player, this._repo.StagedNames[newName]);

            this._repo.Save();
            Assert.AreEqual(1, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);
            Assert.IsTrue(this._repo.SavedNames.ContainsKey(newName));
        }

        [Test]
        public void TestUpdateSad()
        {
            // Create a new player with a new name.
            var n = new Name("asdf");
            var p = new Player(n);
            this._repo.Insert(p);
            // Save them such that they have an ID and can be updated.
            this._repo.Save();

            // Throw _player with a new name into the staged to cause the
            // update exception to get tripped up.
            var a = new Name("fdas");
            this._player.Name = a;
            this._repo.Update(this._player);
            p.Name = a;
            Assert.Throws<ArgumentException>(()=>this._repo.Update(p));
        }

        [Test]
        public void TestUpdateReallocateName()
        {
            var name1 = new Name("second");
            this._player.Name = name1;
            this._repo.Update(this._player);

            var p1 = new Player(this._name);
            this._repo.Insert(p1);

            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);
            this._assertPlayersEqual(p1, this._repo.GetByName(this._name));
            this._assertPlayersEqual(this._player, this._repo.GetByName(name1));
        }

        [Test]
        public void TestDeleteNameTrickery()
        {
            var name = new Name("second");
            var p = new Player(name);
            this._repo.Insert(p);
            this._repo.Save();

            this._repo.Delete(this._player.Id);
            this._repo.Delete(p.Id);
            Assert.AreEqual(2, this._repo.SavedNames.Count);
            Assert.AreEqual(2, this._repo.StagedNames.Count);

            var p1 = new Player(this._name);
            var p2 = new Player(name);
            this._repo.Insert(p1);
            this._repo.Insert(p2);
            Assert.AreEqual(2, this._repo.SavedNames.Count);
            Assert.AreEqual(2, this._repo.StagedNames.Count);

            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);
            this._assertPlayersEqual(p1, this._repo.GetByName(this._name));
            this._assertPlayersEqual(p2, this._repo.GetByName(name));
        }

        [Test]
        public void TestDeleteInvalidId()
        {
            var repo = new TestRAMIdNamedEntityRepo();
            this._repo.Delete(new Id(34));
            Assert.AreEqual(0, repo.SavedNames.Count);
            Assert.AreEqual(0, repo.StagedNames.Count);
            repo.Save();
            Assert.AreEqual(0, repo.SavedNames.Count);
            Assert.AreEqual(0, repo.StagedNames.Count);
        }

        [Test]
        public void TestDeleteHappy()
        {
            this._repo.Delete(this._player.Id);
            Assert.AreEqual(1, this._repo.SavedNames.Count);
            Assert.AreEqual(1, this._repo.StagedNames.Count);
            Assert.IsNull(this._repo.StagedNames[this._player.Name]);
            Assert.AreEqual(0, this._repo.RecycledNames.Count);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.SavedNames.Count);
            Assert.AreEqual(0, this._repo.StagedNames.Count);
        }
    }

    public class TestRAMIdNamedEntityRepo
        : IRAMIdNamedEntityRepo<Player>
    {
        public TestRAMIdNamedEntityRepo()
            : base()
        { }

        public Dictionary<Name, Player> SavedNames
        { get { return this._savedNames; } }
        public Dictionary<Name, Player> StagedNames
        { get { return this._stagedNames; } }

        public Dictionary<Name, int> RecycledNames
        { get { return this._recycledNames; } }
    }
}