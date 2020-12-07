using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestPlayerDel
    {
        private void _absentt<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> getById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private void _allAbsentt()
        {
            this._absentt<UnsafePlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._absentt<UnsafePlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._char1_0, this._charRepo.GetById);
        }

        private void _allPresent()
        {
            this._present<UnsafePlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._present<UnsafePlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._char1_0, this._charRepo.GetById);
        }

        private RAMUnsafePlayerRepo _playerRepo;
        private RAMUnsafeCharacterRepo _charRepo;
        private RAMUnsafeFriendRepo _friendRepo;
        private FriendDel _friendDel;
        private RAMUnsafeFoeRepo _foeRepo;
        private FoeDel _foeDel;
        private CharacterDel _charDel;
        private PlayerDel _del;
        private RAMUnsafePraxisRepo _praxisRepo;
        private RAMPraxisParticipantRepo _ppRepo;
        private PraxisParticipantDel _ppDel;
        private RAMUnsafeCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private RAMUnsafeVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMPraxisTagRepo _pTagRepo;
        private PraxisTagDel _pTagDel;
        private RAMUnsafePraxisFlagRepo _pFlagRepo;
        private PraxisFlagDel _pFlagDel;
        private PraxisDel _pDel;

        private UnsafePlayer _player0;
        private UnsafePlayer _player1;
        private UnsafeCharacter _char0_0;
        private UnsafeCharacter _char0_1;
        private UnsafeCharacter _char1_0;

        [SetUp]
        public void Setup()
        {
            this._charRepo = new RAMUnsafeCharacterRepo();
            this._friendRepo = new RAMUnsafeFriendRepo();
            this._friendDel = new FriendDel(this._friendRepo);
            this._foeRepo = new RAMUnsafeFoeRepo();
            this._foeDel = new FoeDel(this._foeRepo);

            this._praxisRepo = new RAMUnsafePraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();;
            this._commentRepo = new RAMUnsafeCommentRepo();;
            this._commentDel = new CommentDel(this._commentRepo);
            this._pTagRepo = new RAMPraxisTagRepo();
            this._pTagDel = new PraxisTagDel(this._pTagRepo);
            this._pFlagRepo = new RAMUnsafePraxisFlagRepo();
            this._pFlagDel = new PraxisFlagDel(this._pFlagRepo);
            this._voteRepo = new RAMUnsafeVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);

            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
                this._voteDel
            );
            this._pDel = new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._pTagDel,
                this._pFlagDel
            );
            this._charDel = new CharacterDel(
                this._charRepo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            );
            this._playerRepo = new RAMUnsafePlayerRepo();
            this._del = new PlayerDel(this._playerRepo, this._charDel);

            this._player0 = new UnsafePlayer(new Name("x"));
            this._player1 = new UnsafePlayer(new Name("f"));
            this._playerRepo.Insert(this._player0);
            this._playerRepo.Insert(this._player1);
            this._playerRepo.Save();

            this._char0_0 = new UnsafeCharacter(new Name("a"), this._player0.Id);
            this._char0_1 = new UnsafeCharacter(new Name("b"), this._player0.Id);
            this._char1_0 = new UnsafeCharacter(new Name("c"), this._player1.Id);
            this._charRepo.Insert(this._char0_0);
            this._charRepo.Insert(this._char0_1);
            this._charRepo.Insert(this._char1_0);
            this._charRepo.Save();

            this._allPresent();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._charRepo.IsTransactionActive())
            {
                this._charRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._charRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            Id id = null;
            UnsafePlayer p = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(p));
        }

        [Test]
        public void TestDelete()
        {
            this._del.Delete(this._player0);
            this._absentt<UnsafePlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._present<UnsafePlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._char1_0, this._charRepo.GetById);
            this._del.Delete(this._player1);
            this._allAbsentt();
        }

        [Test]
        public void TestConstructor()
        {
            new PlayerDel(
                this._playerRepo,
                this._charDel
            );
            Assert.Throws<ArgumentNullException>(()=>new PlayerDel(
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new PlayerDel(
                null,
                this._charDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PlayerDel(
                this._playerRepo,
                null
            ));
        }
    }
}