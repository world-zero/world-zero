using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using WorldZero.Service.Entity.Update.Primary;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

// NOTE: This does not test `Unset()` as that is done by Location.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestFactionUnset
    {
        private int _nxt = 10000;
        private Id _next() => new Id(this._nxt++);

        private void _absentt<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> getById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ABCSingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ABCSingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private RAMPraxisParticipantRepo _ppRepo;
        private PraxisParticipantDel _ppDel;
        private RAMVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMFactionRepo _factionRepo;
        private RAMMetaTaskRepo _mtRepo;
        private RAMCharacterRepo _charRepo;
        private RAMPraxisRepo _praxisRepo;
        private RAMStatusRepo _statusRepo;
        private RAMLocationRepo _locationRepo;
        private CharacterUpdate _charUpdate;
        private PraxisUpdate _praxisUpdate;
        private RAMTaskRepo _taskRepo;
        private MetaTaskUnset _mtUnset;
        private RAMTaskTagRepo _taskTagRepo;
        private TaskTagDel _taskTagDel;
        private RAMTaskFlagRepo _taskFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private RAMPraxisTagRepo _praxisTagRepo;
        private PraxisTagDel _praxisTagDel;
        private RAMPraxisFlagRepo _praxisFlagRepo;
        private PraxisFlagDel _praxisFlagDel;
        private RAMCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private PraxisDel _praxisDel;
        private TaskDel _taskDel;
        private FactionUnset _unset;
        private UnsafeFaction _faction0;
        private UnsafeFaction _faction1;
        private UnsafeMetaTask _mt0_0;
        private UnsafeMetaTask _mt0_1;
        private UnsafeMetaTask _mt1_0;
        private UnsafeTask _t0_0;
        private UnsafeTask _t1_0;
        private UnsafeTask _t1_1;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMFactionRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._charRepo = new RAMCharacterRepo();
            this._taskRepo = new RAMTaskRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._statusRepo = new RAMStatusRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._praxisTagRepo = new RAMPraxisTagRepo();
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);
            this._praxisFlagRepo = new RAMPraxisFlagRepo();
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);
            this._commentRepo = new RAMCommentRepo();
            var cfDel = new CommentFlagDel(new RAMCommentFlagRepo());
            this._commentDel = new CommentDel(this._commentRepo, cfDel);
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._statusRepo = new RAMStatusRepo();
            this._locationRepo = new RAMLocationRepo();
            this._charUpdate = new CharacterUpdate(
                this._charRepo,
                this._factionRepo,
                this._locationRepo
            );
            this._praxisUpdate = new PraxisUpdate(
                this._praxisRepo,
                this._ppRepo,
                this._statusRepo,
                this._mtRepo,
                this._charRepo,
                this._charUpdate
            );
            this._mtUnset = new MetaTaskUnset(
                this._mtRepo,
                this._praxisRepo,
                this._praxisUpdate
            );
            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
                this._praxisUpdate,
                this._voteDel
            );
            this._praxisDel = new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._praxisTagDel,
                this._praxisFlagDel
            );
            this._taskTagRepo = new RAMTaskTagRepo();
            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._taskFlagRepo = new RAMTaskFlagRepo();
            this._taskFlagDel = new TaskFlagDel(this._taskFlagRepo);
            this._taskDel = new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            );
            this._charUpdate = new CharacterUpdate(
                this._charRepo,
                this._factionRepo,
                this._locationRepo
            );
            this._unset = new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._charUpdate,
                this._taskDel,
                this._mtUnset
            );

            var s = new Name("status");
            var pt = new PointTotal(2);
            var level = new Level(2);

            this._faction0 = new UnsafeFaction(new Name("0"));
            this._faction1 = new UnsafeFaction(new Name("1"));
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Insert(this._faction1);
            this._factionRepo.Save();

            this._mt0_0 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt0_1 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt1_0 = new UnsafeMetaTask(this._faction1.Id, s, "x", pt);
            this._mtRepo.Insert(this._mt0_0);
            this._mtRepo.Insert(this._mt0_1);
            this._mtRepo.Insert(this._mt1_0);
            this._mtRepo.Save();

            this._t0_0 = new UnsafeTask(this._faction0.Id, s, "x", pt, level);
            this._t1_0 = new UnsafeTask(this._faction1.Id, s, "x", pt, level);
            this._t1_1 = new UnsafeTask(this._faction1.Id, s, "x", pt, level);
            this._taskRepo.Insert(this._t0_0);
            this._taskRepo.Insert(this._t1_0);
            this._taskRepo.Insert(this._t1_1);
            this._taskRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._mtRepo.IsTransactionActive())
            {
                this._mtRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._mtRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            Name name = null;
            UnsafeFaction faction = null;
            Assert.Throws<ArgumentNullException>(()=>this._unset.Delete(name));
            Assert.Throws<ArgumentNullException>(()=>this._unset.Delete(faction));

            this._unset.Delete(new Name("faaaaakeeee 13"));
        }

        [Test]
        public void TestDelete_faction0()
        {
            this._unset.Delete(this._faction0);
            this._absentt<IMetaTask, Id, int>(this._mt0_0, this._mtRepo.GetById);
            this._absentt<IMetaTask, Id, int>(this._mt0_1, this._mtRepo.GetById);
            this._present<IMetaTask, Id, int>(this._mt1_0, this._mtRepo.GetById);
            this._absentt<ITask, Id, int>(this._t0_0, this._taskRepo.GetById);
            this._present<ITask, Id, int>(this._t1_0, this._taskRepo.GetById);
            this._present<ITask, Id, int>(this._t1_1, this._taskRepo.GetById);
        }

        [Test]
        public void TestDelete_faction1()
        {
            this._unset.Delete(this._faction1);
            this._present<IMetaTask, Id, int>(this._mt0_0, this._mtRepo.GetById);
            this._present<IMetaTask, Id, int>(this._mt0_1, this._mtRepo.GetById);
            this._absentt<IMetaTask, Id, int>(this._mt1_0, this._mtRepo.GetById);
            this._present<ITask, Id, int>(this._t0_0, this._taskRepo.GetById);
            this._absentt<ITask, Id, int>(this._t1_0, this._taskRepo.GetById);
            this._absentt<ITask, Id, int>(this._t1_1, this._taskRepo.GetById);
        }

        [Test]
        public void TestConstructor()
        {
            new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._charUpdate,
                this._taskDel,
                this._mtUnset
            );
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                null,
                this._charRepo,
                this._charUpdate,
                this._taskDel,
                this._mtUnset
            ));
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                this._factionRepo,
                null,
                this._charUpdate,
                this._taskDel,
                this._mtUnset
            ));
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                this._factionRepo,
                this._charRepo,
                null,
                this._taskDel,
                this._mtUnset
            ));
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._charUpdate,
                this._taskDel,
                null
            ));
        }
    }
}