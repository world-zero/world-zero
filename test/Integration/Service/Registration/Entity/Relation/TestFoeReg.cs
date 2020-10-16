using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Registration.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity.Relation
{
    [TestFixture]
    public class TestFoeReg
    {
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Id _id4;
        private IFriendRepo _friendRepo;
        private ICharacterRepo _characterRepo;
        private IFoeRepo _foeRepo;
        private FoeReg _foeReg;

        [SetUp]
        public void Setup()
        {
            this._id1 = new Id(1);
            this._id2 = new Id(2);
            this._id3 = new Id(3);
            this._id4 = new Id(4);
            this._friendRepo = new DummyRAMFriendRepo();
            this._foeRepo = new DummyRAMFoeRepo();
            this._characterRepo = new DummyRAMCharacterRepo();
            this._foeReg = new FoeReg(
                this._foeRepo,
                this._characterRepo,
                this._friendRepo
            );
        }

        [TearDown]
        public void TearDown()
        {
            this._foeRepo.CleanAll();
            ((DummyRAMFriendRepo) this._friendRepo).ResetNextIdValue();
            ((DummyRAMFoeRepo) this._foeRepo).ResetNextIdValue();
            ((DummyRAMCharacterRepo) this._characterRepo).ResetNextIdValue();
        }

        [Test]
        public void TestRegisterHappy()
        {
            this._characterRepo.Insert(new Character(
                new Name("first"), new Id(43)
            ));
            this._characterRepo.Insert(new Character(
                new Name("second"), new Id(43)
            ));
            this._characterRepo.Save();
            var f0 = new Foe(this._id1, this._id2);
            this._foeReg.Register(f0);
            var f1 = this._foeRepo.GetByDTO(
                new RelationDTO<Id, int, Id, int>(this._id1, this._id2));
            Assert.AreEqual(f0.Id, f1.Id);
            Assert.AreEqual(f0, f1);
        }

        [Test]
        public void TestFriendsCantBecomeFoes()
        {
            this._characterRepo.Insert(new Character(
                new Name("first"), new Id(43)
            ));
            this._characterRepo.Insert(new Character(
                new Name("second"), new Id(43)
            ));
            this._characterRepo.Save();
            var friend0 = new Friend(this._id1, this._id2);
            this._friendRepo.Insert(friend0);
            this._friendRepo.Save();

            Assert.Throws<ArgumentException>(()=>
                this._foeReg.Register(new Foe(this._id1, this._id2)));
            Assert.Throws<ArgumentException>(()=>
                this._foeReg.Register(new Foe(this._id2, this._id1)));
        }
    }

    public class DummyRAMFriendRepo
        : RAMFriendRepo
    {
        public DummyRAMFriendRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }

    public class DummyRAMFoeRepo
        : RAMFoeRepo
    {
        public DummyRAMFoeRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }

    public class DummyRAMCharacterRepo
        : RAMCharacterRepo
    {
        public DummyRAMCharacterRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }
}