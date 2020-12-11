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

// NOTE: `PraxisDel.DeleteByStatus()` is tested in place of
// `IIdStatusedEntityDel.DeleteByStatus()`.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestPraxisDel
    {
        private int _nxt = 10000;
        private Id _next() => new Id(this._nxt++);

        private void _absentt<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> getById)
            where TEntity : ABCEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : ABCEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private void _allAbsentt()
        {
            this._absentt<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

        private void _allPresent()
        {
            this._present<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

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
        private PraxisDel _del;

        private Id _taskId0;
        private Id _taskId1;
        private Name _statusId0;
        private Name _statusId1;
        private UnsafePraxis _p0;
        private UnsafePraxis _p1;
        private UnsafeComment _comment0_0;
        private UnsafeComment _comment0_1;
        private UnsafeComment _comment1_0;
        private UnsafePraxisTag _pTag0_0;
        private UnsafePraxisTag _pTag0_1;
        private UnsafePraxisTag _pTag1_0;
        private UnsafePraxisFlag _pFlag0_0;
        private UnsafePraxisFlag _pFlag1_0;
        private UnsafePraxisFlag _pFlag1_1;
        private UnsafePraxisParticipant _pp0_0;
        private UnsafePraxisParticipant _pp0_1;
        private UnsafePraxisParticipant _pp1_0;
        // Votes are tested as well to ensure that the PP deletion is cascaded.
        private UnsafeVote _v0_0;
        private UnsafeVote _v0_1;
        private UnsafeVote _v1_0;

        [SetUp]
        public void Setup()
        {
            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();;
            this._commentRepo = new RAMCommentRepo();;
            this._commentDel = new CommentDel(this._commentRepo);
            this._pTagRepo = new RAMPraxisTagRepo();
            this._pTagDel = new PraxisTagDel(this._pTagRepo);
            this._pFlagRepo = new RAMPraxisFlagRepo();
            this._pFlagDel = new PraxisFlagDel(this._pFlagRepo);

            // Now we build two praxises, each with comments, votes,
            // tags, flags, and participants.
            var pt = new PointTotal(2);
            this._statusId0 = new Name("good");
            this._statusId1 = new Name("also good");
            this._taskId0 = this._next();
            this._taskId1 = this._next();
            this._p0 = new UnsafePraxis(this._taskId0, pt, this._statusId0);
            this._p1 = new UnsafePraxis(this._taskId1, pt, this._statusId1);
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
            this._comment0_0 = new UnsafeComment(this._p0.Id, this._next(), "f");
            this._comment0_1 = new UnsafeComment(this._p0.Id, this._next(), "f f");
            this._comment1_0 = new UnsafeComment(this._p1.Id, this._next(), "x");
            this._commentRepo.Insert(this._comment0_0);
            this._commentRepo.Insert(this._comment0_1);
            this._commentRepo.Insert(this._comment1_0);
            this._commentRepo.Save();

            this._pTag0_0 = new UnsafePraxisTag(this._p0.Id, new Name("#lit"));
            this._pTag0_1 = new UnsafePraxisTag(this._p0.Id, new Name("#pog"));
            this._pTag1_0 = new UnsafePraxisTag(this._p1.Id, new Name("#swoot"));
            this._pTagRepo.Insert(this._pTag0_0);
            this._pTagRepo.Insert(this._pTag0_1);
            this._pTagRepo.Insert(this._pTag1_0);
            this._pTagRepo.Save();

            this._pFlag0_0 = new UnsafePraxisFlag(this._p0.Id, new Name("hawt"));
            this._pFlag1_0 = new UnsafePraxisFlag(this._p1.Id, new Name("tasty"));
            this._pFlag1_1 = new UnsafePraxisFlag(this._p1.Id, new Name("beef"));
            this._pFlagRepo.Insert(this._pFlag0_0);
            this._pFlagRepo.Insert(this._pFlag1_0);
            this._pFlagRepo.Insert(this._pFlag1_1);
            this._pFlagRepo.Save();

            this._pp0_0 = new UnsafePraxisParticipant(this._p0.Id, this._next());
            this._pp0_1 = new UnsafePraxisParticipant(this._p0.Id, this._next());
            this._pp1_0 = new UnsafePraxisParticipant(this._p1.Id, this._next());
            this._ppRepo.Insert(this._pp0_0);
            this._ppRepo.Insert(this._pp0_1);
            this._ppRepo.Insert(this._pp1_0);
            this._ppRepo.Save();

            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._v0_0 = new UnsafeVote(this._next(), this._pp0_0.Id, pt);
            this._v0_1 = new UnsafeVote(this._next(), this._pp0_1.Id, pt);
            this._v1_0 = new UnsafeVote(this._next(), this._pp1_0.Id, pt);
            this._voteRepo.Insert(this._v0_0);
            this._voteRepo.Insert(this._v0_1);
            this._voteRepo.Insert(this._v1_0);
            this._voteRepo.Save();

            this._allPresent();

            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
                this._voteDel
            );
            this._del = new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._pTagDel,
                this._pFlagDel
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._praxisRepo.IsTransactionActive())
            {
                this._praxisRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._praxisRepo.CleanAll();
        }

        [Test]
        public void TestDelete_p0()
        {
            this._del.Delete(this._p0);
            this._absentt<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

        [Test]
        public void TestDelete_p1()
        {
            this._del.Delete(this._p1);
            this._present<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

        [Test]
        public void TestDeleteBothPraxises()
        {
            this._del.Delete(this._p0);
            this._del.Delete(this._p1);
            this._allAbsentt();
        }

        [Test]
        public void TestDeleteSad()
        {
            Id id = null;
            UnsafePraxis p = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(p));
            this._del.Delete(new Id(10000));
        }

        [Test]
        public void TestDeleteByTaskSadBad()
        {
            Id id = null;
            UnsafeTask t = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTask(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTask(t));

            this._del.DeleteByTask(new Id(10000000));
            this._allPresent();
        }

        // Sure, these next two aren't super in-depth, but it's fine
        //      [famous last words, I know].
        [Test]
        public void TestDeleteByTask_taskId0()
        {
            this._del.DeleteByTask(this._p0.TaskId);
            this._absentt<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

        [Test]
        public void TestDeleteByTask_taskId1()
        {
            this._del.DeleteByTask(this._p1.TaskId);
            this._present<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
        }

        [Test]
        public void TestDeleteByStatus()
        {
            this._del.DeleteByStatus(this._p0.StatusId);
            this._absentt<UnsafePraxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<UnsafeComment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<UnsafeComment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<UnsafePraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<UnsafePraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<UnsafePraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_0, this._voteRepo.GetById);
            this._absentt<UnsafeVote, Id, int>(this._v0_1, this._voteRepo.GetById);
            this._present<UnsafeVote, Id, int>(this._v1_0, this._voteRepo.GetById);
            this._del.DeleteByStatus(this._p0.StatusId);
        }

        [Test]
        public void TestConstructor()
        {
            new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._pTagDel,
                this._pFlagDel
            );
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                null,
                null,
                null,
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                null,
                this._ppDel,
                this._commentDel,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                null,
                this._commentDel,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                null,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                null,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._pTagDel,
                null
            ));
        }
    }
}