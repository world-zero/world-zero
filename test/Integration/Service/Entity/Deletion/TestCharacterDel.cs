using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestCharacterDel
    {
        private int _nxt = 10000;
        private Id _next() => new Id(this._nxt++);

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
            this._absentt<UnsafeCharacter, Id, int>(this._c0_0, this._repo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._c0_1, this._repo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._c1_0, this._repo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_01, this._friendRepo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend10_01, this._friendRepo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_10, this._friendRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_01, this._foeRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe10_01, this._foeRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_10, this._foeRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis00_10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis00, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis01, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0010_00, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0010_10, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp00, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp01, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp10, this._ppRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10Again, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy01on10, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy01on10Again, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00Again, this._commentRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy00on10, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy01on10, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy10on00, this._voteRepo.GetById);
        }

        private void _allPresent()
        {
            this._present<UnsafeCharacter, Id, int>(this._c0_0, this._repo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._c0_1, this._repo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._c1_0, this._repo.GetById);
            this._present<UnsafeFriend, Id, int>(this._friend00_01, this._friendRepo.GetById);
            this._present<UnsafeFriend, Id, int>(this._friend10_01, this._friendRepo.GetById);
            this._present<UnsafeFriend, Id, int>(this._friend00_10, this._friendRepo.GetById);
            this._present<UnsafeFoe, Id, int>(this._foe00_01, this._foeRepo.GetById);
            this._present<UnsafeFoe, Id, int>(this._foe10_01, this._foeRepo.GetById);
            this._present<UnsafeFoe, Id, int>(this._foe00_10, this._foeRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis00_10, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis00, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis01, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis10, this._praxisRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0010_00, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0010_10, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp00, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp01, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp10, this._ppRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy00on10, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy00on10Again, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy01on10, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy01on10Again, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy10on00, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy10on00Again, this._commentRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._voteBy00on10, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._voteBy01on10, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._voteBy10on00, this._voteRepo.GetById);
        }

        private Id _pId0;
        private Id _pId1;
        private UnsafeCharacter _c0_0;
        private UnsafeCharacter _c0_1;
        private UnsafeCharacter _c1_0;
        private UnsafeFriend _friend00_01;
        private UnsafeFriend _friend10_01;
        private UnsafeFriend _friend00_10;
        private UnsafeFoe _foe00_01;
        private UnsafeFoe _foe10_01;
        private UnsafeFoe _foe00_10;
        private UnsafePraxis _praxis00_10;
        private UnsafePraxis _praxis00;
        private UnsafePraxis _praxis01;
        private UnsafePraxis _praxis10;
        private UnsafePraxisParticipant _pp0010_00;
        private UnsafePraxisParticipant _pp0010_10;
        private UnsafePraxisParticipant _pp00;
        private UnsafePraxisParticipant _pp01;
        private UnsafePraxisParticipant _pp10;
        private UnsafeComment _commentBy00on10;
        private UnsafeComment _commentBy00on10Again;
        private UnsafeComment _commentBy01on10;
        private UnsafeComment _commentBy01on10Again;
        private UnsafeComment _commentBy10on00;
        private UnsafeComment _commentBy10on00Again;
        private UnsafeVote _voteBy00on10;
        private UnsafeVote _voteBy01on10;
        private UnsafeVote _voteBy10on00;

        private RAMUnsafeCharacterRepo _repo;
        private RAMUnsafeFriendRepo _friendRepo;
        private FriendDel _friendDel;
        private RAMUnsafeFoeRepo _foeRepo;
        private FoeDel _foeDel;
        private CharacterDel _del;
        private RAMUnsafePraxisRepo _praxisRepo;
        private RAMUnsafePraxisParticipantRepo _ppRepo;
        private PraxisParticipantDel _ppDel;
        private RAMUnsafeCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private RAMUnsafeVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMUnsafePraxisTagRepo _pTagRepo;
        private PraxisTagDel _pTagDel;
        private RAMUnsafePraxisFlagRepo _pFlagRepo;
        private PraxisFlagDel _pFlagDel;
        private PraxisDel _pDel;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMUnsafeCharacterRepo();
            this._friendRepo = new RAMUnsafeFriendRepo();
            this._friendDel = new FriendDel(this._friendRepo);
            this._foeRepo = new RAMUnsafeFoeRepo();
            this._foeDel = new FoeDel(this._foeRepo);

            this._praxisRepo = new RAMUnsafePraxisRepo();
            this._ppRepo = new RAMUnsafePraxisParticipantRepo();;
            this._commentRepo = new RAMUnsafeCommentRepo();;
            this._commentDel = new CommentDel(this._commentRepo);
            this._pTagRepo = new RAMUnsafePraxisTagRepo();
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
            this._del = new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            );

            this._pId0 = new Id(20);
            this._pId1 = new Id(30);
            this._c0_0 = new UnsafeCharacter(new Name("a"), this._pId0);
            this._c0_1 = new UnsafeCharacter(new Name("b"), this._pId0);
            this._c1_0 = new UnsafeCharacter(new Name("c"), this._pId1);
            this._repo.Insert(this._c0_0);
            this._repo.Insert(this._c0_1);
            this._repo.Insert(this._c1_0);
            this._repo.Save();

            this._friend00_01 = new UnsafeFriend(this._c0_0.Id, this._c0_1.Id);
            this._friend10_01 = new UnsafeFriend(this._c1_0.Id, this._c0_1.Id);
            this._friend00_10 = new UnsafeFriend(this._c0_0.Id, this._c1_0.Id);
            this._friendRepo.Insert(this._friend00_01);
            this._friendRepo.Insert(this._friend00_10);
            this._friendRepo.Insert(this._friend10_01);
            this._friendRepo.Save();

            this._foe00_01 = new UnsafeFoe(this._c0_0.Id, this._c0_1.Id);
            this._foe10_01 = new UnsafeFoe(this._c1_0.Id, this._c0_1.Id);
            this._foe00_10 = new UnsafeFoe(this._c0_0.Id, this._c1_0.Id);
            this._foeRepo.Insert(this._foe00_01);
            this._foeRepo.Insert(this._foe00_10);
            this._foeRepo.Insert(this._foe10_01);
            this._foeRepo.Save();

            var taskId0 = new Id(34);
            var pt = new PointTotal(2);
            var status = new Name("vaid");
            this._praxis00_10 = new UnsafePraxis(taskId0, pt, status, areDueling: true);
            this._praxis00 = new UnsafePraxis(taskId0, pt, status);
            this._praxis10 = new UnsafePraxis(taskId0, pt, status);
            this._praxis01 = new UnsafePraxis(taskId0, pt, status);
            this._praxisRepo.Insert(this._praxis00_10);
            this._praxisRepo.Insert(this._praxis00);
            this._praxisRepo.Insert(this._praxis01);
            this._praxisRepo.Insert(this._praxis10);
            this._praxisRepo.Save();
            
            this._pp0010_00 = new UnsafePraxisParticipant(this._praxis00_10.Id, this._c0_0.Id);
            this._pp0010_10 = new UnsafePraxisParticipant(this._praxis00_10.Id, this._c1_0.Id);
            this._pp00 = new UnsafePraxisParticipant(this._praxis00.Id, this._c0_0.Id);
            this._pp01 = new UnsafePraxisParticipant(this._praxis01.Id, this._c0_1.Id);
            this._pp10 = new UnsafePraxisParticipant(this._praxis10.Id, this._c1_0.Id);
            this._ppRepo.Insert(this._pp0010_00);
            this._ppRepo.Insert(this._pp0010_10);
            this._ppRepo.Insert(this._pp00);
            this._ppRepo.Insert(this._pp01);
            this._ppRepo.Insert(this._pp10);
            this._ppRepo.Save();

            this._commentBy00on10 = new UnsafeComment(this._praxis10.Id, this._c0_0.Id, "x");
            this._commentBy00on10Again = new UnsafeComment(this._praxis10.Id, this._c0_0.Id, "x", 2);
            this._commentBy01on10 = new UnsafeComment(this._praxis10.Id, this._c0_1.Id, "x");
            this._commentBy01on10Again = new UnsafeComment(this._praxis10.Id, this._c0_1.Id, "x", 2);
            this._commentBy10on00 = new UnsafeComment(this._praxis00.Id, this._c1_0.Id, "x");
            this._commentBy10on00Again = new UnsafeComment(this._praxis00.Id, this._c1_0.Id, "x", 2);
            this._commentRepo.Insert(this._commentBy00on10);
            this._commentRepo.Insert(this._commentBy00on10Again);
            this._commentRepo.Insert(this._commentBy01on10);
            this._commentRepo.Insert(this._commentBy01on10Again);
            this._commentRepo.Insert(this._commentBy10on00);
            this._commentRepo.Insert(this._commentBy10on00Again);
            this._commentRepo.Save();

            this._voteBy00on10 = new UnsafeVote(this._c0_0.Id, this._pp10.Id, pt);
            this._voteBy01on10 = new UnsafeVote(this._c0_1.Id, this._pp10.Id, pt);
            this._voteBy10on00 = new UnsafeVote(this._c1_0.Id, this._pp00.Id, pt);
            this._voteRepo.Insert(this._voteBy00on10);
            this._voteRepo.Insert(this._voteBy01on10);
            this._voteRepo.Insert(this._voteBy10on00);
            this._voteRepo.Save();

            this._allPresent();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            Id id = null;
            UnsafeCharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(c));
        }

        [Test]
        public void TestDelete()
        {
            this._del.Delete(this._c0_0);
            this._absentt<UnsafeCharacter, Id, int>(this._c0_0, this._repo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._c0_1, this._repo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._c1_0, this._repo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_01, this._friendRepo.GetById);
            this._present<UnsafeFriend, Id, int>(this._friend10_01, this._friendRepo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_10, this._friendRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_01, this._foeRepo.GetById);
            this._present<UnsafeFoe, Id, int>(this._foe10_01, this._foeRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_10, this._foeRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis00_10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis00, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis01, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0010_00, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0010_10, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp00, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp01, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp10, this._ppRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10Again, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy01on10, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._commentBy01on10Again, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00Again, this._commentRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy00on10, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._voteBy01on10, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy10on00, this._voteRepo.GetById);
            Assert.AreEqual(1, this._ppRepo.GetParticipantCountViaPraxisId(this._praxis00_10.Id));
            var praxis00_10 = this._praxisRepo.GetById(this._praxis00_10.Id);
            Assert.IsFalse(praxis00_10.AreDueling);
        }

        [Test]
        public void TestDeleteByPlayerSad()
        {
            Id id = null;
            UnsafePlayer p = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByPlayer(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByPlayer(p));
        }

        [Test]
        public void TestDeleteByPlayer()
        {
            this._del.DeleteByPlayer(this._pId0);
            this._absentt<UnsafeCharacter, Id, int>(this._c0_0, this._repo.GetById);
            this._absentt<UnsafeCharacter, Id, int>(this._c0_1, this._repo.GetById);
            this._present<UnsafeCharacter, Id, int>(this._c1_0, this._repo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_01, this._friendRepo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend10_01, this._friendRepo.GetById);
            this._absentt<UnsafeFriend, Id, int>(this._friend00_10, this._friendRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_01, this._foeRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe10_01, this._foeRepo.GetById);
            this._absentt<UnsafeFoe, Id, int>(this._foe00_10, this._foeRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis00_10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis00, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis01, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis10, this._praxisRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0010_00, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0010_10, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp00, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp01, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp10, this._ppRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy00on10Again, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy01on10, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy01on10Again, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._commentBy10on00Again, this._commentRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy00on10, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy01on10, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._voteBy10on00, this._voteRepo.GetById);
            Assert.AreEqual(1, this._ppRepo.GetParticipantCountViaPraxisId(this._praxis00_10.Id));
            var praxis00_10 = this._praxisRepo.GetById(this._praxis00_10.Id);
            Assert.IsFalse(praxis00_10.AreDueling);
        }

        [Test]
        public void TestConstructor()
        {
            new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            );
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                null,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                null,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                this._friendDel,
                null,
                this._commentDel,
                this._voteDel,
                this._pDel,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                null,
                this._voteDel,
                this._pDel,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                null,
                this._pDel,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                null,
                this._ppDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                this._repo,
                this._friendDel,
                this._foeDel,
                this._commentDel,
                this._voteDel,
                this._pDel,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new CharacterDel(
                null,
                null,
                null,
                null,
                null,
                null,
                null
            ));
        }
    }
}