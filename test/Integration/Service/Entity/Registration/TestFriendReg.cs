using System;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Entity.Registration.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestFriendReg
    {
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Id _id4;
        private IUnsafeFriendRepo _friendRepo;
        private IUnsafeCharacterRepo _characterRepo;
        private IUnsafeFoeRepo _foeRepo;
        private FriendReg _friendReg;

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
            this._friendReg = new FriendReg(
                this._friendRepo,
                this._characterRepo,
                this._foeRepo
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._foeRepo.IsTransactionActive())
            {
                this._foeRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._foeRepo.CleanAll();
            ((DummyRAMFriendRepo) this._friendRepo).ResetNextIdValue();
            ((DummyRAMFoeRepo) this._foeRepo).ResetNextIdValue();
            ((DummyRAMCharacterRepo) this._characterRepo).ResetNextIdValue();
        }

        [Test]
        public void TestRegisterHappy()
        {
            this._characterRepo.Insert(new UnsafeCharacter(
                new Name("first"), new Id(43)
            ));
            this._characterRepo.Insert(new UnsafeCharacter(
                new Name("second"), new Id(43)
            ));
            this._characterRepo.Save();
            var f0 = new UnsafeFriend(this._id1, this._id2);
            this._friendReg.Register(f0);
            var f1 = this._friendRepo.GetByDTO(
                new RelationDTO<Id, int, Id, int>(this._id1, this._id2));
            Assert.AreEqual(f0.Id, f1.Id);
            Assert.AreEqual(f0, f1);
        }

        [Test]
        public void TestFoesCantBecomeFriends()
        {
            this._characterRepo.Insert(new UnsafeCharacter(
                new Name("first"), new Id(43)
            ));
            this._characterRepo.Insert(new UnsafeCharacter(
                new Name("second"), new Id(43)
            ));
            this._characterRepo.Save();
            var foe0 = new UnsafeFoe(this._id1, this._id2);
            this._foeRepo.Insert(foe0);
            this._foeRepo.Save();

            Assert.Throws<ArgumentException>(()=>
                this._friendReg.Register(new UnsafeFriend(this._id1, this._id2)));
            Assert.Throws<ArgumentException>(()=>
                this._friendReg.Register(new UnsafeFriend(this._id2, this._id1)));
        }
    }
}