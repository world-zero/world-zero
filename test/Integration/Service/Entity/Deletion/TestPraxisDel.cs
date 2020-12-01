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
            this._absentt<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        private void _allPresent()
        {
            this._present<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        private RAMPraxisRepo _praxisRepo;
        private RAMPraxisParticipantRepo _ppRepo;
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
        private Praxis _p0;
        private Praxis _p1;
        private Comment _comment0_0;
        private Comment _comment0_1;
        private Comment _comment1_0;
        private Vote _vote0_0;
        private Vote _vote1_0;
        private Vote _vote1_1;
        private PraxisTag _pTag0_0;
        private PraxisTag _pTag0_1;
        private PraxisTag _pTag1_0;
        private PraxisFlag _pFlag0_0;
        private PraxisFlag _pFlag1_0;
        private PraxisFlag _pFlag1_1;
        private PraxisParticipant _pp0_0;
        private PraxisParticipant _pp0_1;
        private PraxisParticipant _pp1_0;

        [SetUp]
        public void Setup()
        {
            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();;
            this._commentRepo = new RAMCommentRepo();;
            this._commentDel = new CommentDel(this._commentRepo);
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._pTagRepo = new RAMPraxisTagRepo();
            this._pTagDel = new PraxisTagDel(this._pTagRepo);
            this._pFlagRepo = new RAMPraxisFlagRepo();
            this._pFlagDel = new PraxisFlagDel(this._pFlagRepo);
            this._del = new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                this._pTagDel,
                this._pFlagDel
            );

            // Now we build two praxises, each with comments, votes,
            // tags, flags, and participants.
            var pt = new PointTotal(2);
            this._statusId0 = new Name("good");
            this._statusId1 = new Name("also good");
            this._taskId0 = this._next();
            this._taskId1 = this._next();
            this._p0 = new Praxis(this._taskId0, pt, this._statusId0);
            this._p1 = new Praxis(this._taskId1, pt, this._statusId1);
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
            this._comment0_0 = new Comment(this._p0.Id, this._next(), "f");
            this._comment0_1 = new Comment(this._p0.Id, this._next(), "f f");
            this._comment1_0 = new Comment(this._p1.Id, this._next(), "x");
            this._commentRepo.Insert(this._comment0_0);
            this._commentRepo.Insert(this._comment0_1);
            this._commentRepo.Insert(this._comment1_0);
            this._commentRepo.Save();

            this._vote0_0 = new Vote(this._next(), this._p0.Id, this._next(), pt);
            this._vote1_0 = new Vote(this._next(), this._p1.Id, this._next(), pt);
            this._vote1_1 = new Vote(this._next(), this._p1.Id, this._next(), pt);
            this._voteRepo.Insert(this._vote0_0);
            this._voteRepo.Insert(this._vote1_0);
            this._voteRepo.Insert(this._vote1_1);
            this._voteRepo.Save();

            this._pTag0_0 = new PraxisTag(this._p0.Id, new Name("#lit"));
            this._pTag0_1 = new PraxisTag(this._p0.Id, new Name("#pog"));
            this._pTag1_0 = new PraxisTag(this._p1.Id, new Name("#swoot"));
            this._pTagRepo.Insert(this._pTag0_0);
            this._pTagRepo.Insert(this._pTag0_1);
            this._pTagRepo.Insert(this._pTag1_0);
            this._pTagRepo.Save();

            this._pFlag0_0 = new PraxisFlag(this._p0.Id, new Name("hawt"));
            this._pFlag1_0 = new PraxisFlag(this._p1.Id, new Name("tasty"));
            this._pFlag1_1 = new PraxisFlag(this._p1.Id, new Name("beef"));
            this._pFlagRepo.Insert(this._pFlag0_0);
            this._pFlagRepo.Insert(this._pFlag1_0);
            this._pFlagRepo.Insert(this._pFlag1_1);
            this._pFlagRepo.Save();

            this._pp0_0 = new PraxisParticipant(this._p0.Id, this._next());
            this._pp0_1 = new PraxisParticipant(this._p0.Id, this._next());
            this._pp1_0 = new PraxisParticipant(this._p1.Id, this._next());
            this._ppRepo.Insert(this._pp0_0);
            this._ppRepo.Insert(this._pp0_1);
            this._ppRepo.Insert(this._pp1_0);
            this._ppRepo.Save();
            this._allPresent();
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
            this._absentt<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDelete_p1()
        {
            this._del.Delete(this._p1);
            this._present<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
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
            Praxis p = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(p));
            this._del.Delete(new Id(10000));
        }

        [Test]
        public void TestDeleteByTaskSadBad()
        {
            Id id = null;
            Task t = null;
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
            this._absentt<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteByTask_taskId1()
        {
            this._del.DeleteByTask(this._p1.TaskId);
            this._present<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._absentt<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._present<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteByStatus()
        {
            this._del.DeleteByStatus(this._p0.StatusId);
            this._absentt<Praxis, Id, int>(this._p0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._p1, this._praxisRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_0, this._commentRepo.GetById);
            this._absentt<Comment, Id, int>(this._comment0_1, this._commentRepo.GetById);
            this._present<Comment, Id, int>(this._comment1_0, this._commentRepo.GetById);
            this._absentt<Vote, Id, int>(this._vote0_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_0, this._voteRepo.GetById);
            this._present<Vote, Id, int>(this._vote1_1, this._voteRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_0, this._pTagRepo.GetById);
            this._absentt<PraxisTag, Id, int>(this._pTag0_1, this._pTagRepo.GetById);
            this._present<PraxisTag, Id, int>(this._pTag1_0, this._pTagRepo.GetById);
            this._absentt<PraxisFlag, Id, int>(this._pFlag0_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_0, this._pFlagRepo.GetById);
            this._present<PraxisFlag, Id, int>(this._pFlag1_1, this._pFlagRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<PraxisParticipant, Id, int>(this._pp0_1, this._ppRepo.GetById);
            this._present<PraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
            this._del.DeleteByStatus(this._p0.StatusId);
        }

        [Test]
        public void TestConstructor()
        {
            new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                this._pTagDel,
                this._pFlagDel
            );
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                null,
                null,
                null,
                null,
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                null,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                null,
                this._commentDel,
                this._voteDel,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                null,
                this._voteDel,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                null,
                this._pTagDel,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                null,
                this._pFlagDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                this._pTagDel,
                null
            ));
        }
    }
}