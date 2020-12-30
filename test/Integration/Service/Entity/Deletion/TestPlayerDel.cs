using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using WorldZero.Service.Entity.Update.Primary;
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
            this._absentt<IPlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._absentt<IPlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._absentt<ICharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._absentt<ICharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._absentt<ICharacter, Id, int>(this._char1_0, this._charRepo.GetById);
        }

        private void _allPresent()
        {
            this._present<IPlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._present<IPlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._present<ICharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._present<ICharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._present<ICharacter, Id, int>(this._char1_0, this._charRepo.GetById);
        }

        private RAMPlayerRepo _playerRepo;
        private RAMCharacterRepo _charRepo;
        private RAMFriendRepo _friendRepo;
        private FriendDel _friendDel;
        private RAMFoeRepo _foeRepo;
        private FoeDel _foeDel;
        private CharacterDel _charDel;
        private PlayerDel _del;
        private RAMPraxisRepo _praxisRepo;
        private RAMPraxisParticipantRepo _ppRepo;
        private PraxisParticipantDel _ppDel;
        private RAMCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private RAMVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMPraxisTagRepo _pTagRepo;
        private PraxisTagDel _pTagDel;
        private RAMPraxisFlagRepo _pFlagRepo;
        private PraxisFlagDel _pFlagDel;
        private PraxisDel _pDel;
        private RAMStatusRepo _statusRepo;
        private RAMMetaTaskRepo _mtRepo;
        private RAMFactionRepo _factionRepo;
        private RAMLocationRepo _locationRepo;
        private CharacterUpdate _charUpdate;
        private PraxisUpdate _praxisUpdate;

        private UnsafePlayer _player0;
        private UnsafePlayer _player1;
        private UnsafeCharacter _char0_0;
        private UnsafeCharacter _char0_1;
        private UnsafeCharacter _char1_0;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMFactionRepo();
            this._locationRepo = new RAMLocationRepo();
            this._charRepo = new RAMCharacterRepo();
            this._charUpdate = new CharacterUpdate(
                this._charRepo,
                this._factionRepo,
                this._locationRepo
            );
            this._friendRepo = new RAMFriendRepo();
            this._friendDel = new FriendDel(this._friendRepo);
            this._foeRepo = new RAMFoeRepo();
            this._foeDel = new FoeDel(this._foeRepo);

            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();;
            this._commentRepo = new RAMCommentRepo();;
            var cfDel = new CommentFlagDel(new RAMCommentFlagRepo());
            this._commentDel = new CommentDel(this._commentRepo, cfDel);
            this._pTagRepo = new RAMPraxisTagRepo();
            this._pTagDel = new PraxisTagDel(this._pTagRepo);
            this._pFlagRepo = new RAMPraxisFlagRepo();
            this._pFlagDel = new PraxisFlagDel(this._pFlagRepo);
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);

            this._statusRepo = new RAMStatusRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._praxisUpdate = new PraxisUpdate(
                this._praxisRepo,
                this._ppRepo,
                this._statusRepo,
                this._mtRepo,
                this._charRepo,
                this._charUpdate
            );
            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
                this._praxisUpdate,
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
            this._playerRepo = new RAMPlayerRepo();
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
            this._absentt<IPlayer, Id, int>(this._player0, this._playerRepo.GetById);
            this._present<IPlayer, Id, int>(this._player1, this._playerRepo.GetById);
            this._absentt<ICharacter, Id, int>(this._char0_0, this._charRepo.GetById);
            this._absentt<ICharacter, Id, int>(this._char0_1, this._charRepo.GetById);
            this._present<ICharacter, Id, int>(this._char1_0, this._charRepo.GetById);
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