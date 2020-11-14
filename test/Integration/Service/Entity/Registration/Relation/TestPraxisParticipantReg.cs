using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Entity.Registration;
using WorldZero.Service.Entity.Registration.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration.Relation
{
    [TestFixture]
    public class TestPraxisParticipantReg
    {
        private PointTotal _pt;
        private RAMTaskRepo _taskRepo;
        private RAMFactionRepo _factionRepo;
        private DummyRAMPraxisParticipantRepo _ppRepo;
        private DummyRAMPraxisRepo _praxisRepo;
        private DummyRAMCharacterRepo _charRepo;
        private DummyRAMMetaTaskRepo _mtRepo;
        private PraxisParticipantReg _ppReg;
        private RAMEraRepo _eraRepo;
        private EraReg _eraReg;
        private Character _c0;
        private Character _c1;
        private Character _c2;
        private Praxis _p0;
        private Praxis _p1;
        private MetaTask _mt;
        private PraxisParticipant _pp;
        private Faction _f;
        private Id _tId;
        private Era _e;

        [SetUp]
        public void Setup()
        {
            this._taskRepo = new RAMTaskRepo();
            this._eraRepo = new RAMEraRepo();
            this._e = new Era(
                new Name("Testing"),
                maxTasks: 1,
                maxTasksReiterator: 2
            );
            this._eraRepo.Insert(this._e);
            this._eraRepo.Save();
            this._eraReg = new EraReg(this._eraRepo);
            this._ppRepo = new DummyRAMPraxisParticipantRepo();
            this._praxisRepo = new DummyRAMPraxisRepo();
            this._charRepo = new DummyRAMCharacterRepo();
            this._mtRepo = new DummyRAMMetaTaskRepo();
            this._factionRepo = new RAMFactionRepo();
            this._ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo,
                this._taskRepo,
                this._factionRepo,
                this._eraReg
            );

            var task = new Task(
                new Name("x"),
                StatusReg.Active.Id,
                "z",
                new PointTotal(2),
                new Level(3)
            );
            this._taskRepo.Insert(task);
            this._taskRepo.Save();
            this._tId = task.Id;
            this._f =
                new Faction(new Name("Good"), new PastDate(DateTime.UtcNow));
            this._factionRepo.Insert(this._f);
            this._factionRepo.Save();
            this._pt = new PointTotal(1000);
            this._c0 = new Character(
                new Name("valid"),
                new Id(1),
                this._f.Id,
                eraPoints: this._pt
            );
            this._c1 = new Character(
                new Name("other"),
                new Id(20),
                eraPoints: this._pt
            );
            this._c2 = new Character(
                new Name("other other"),
                new Id(10),
                eraPoints: this._pt
            );
            this._charRepo.Insert(this._c0);
            this._charRepo.Insert(this._c1);
            this._charRepo.Insert(this._c2);
            this._charRepo.Save();

            this._mt = new MetaTask(
                this._f.Id,
                StatusReg.Active.Id,
                "x",
                new PointTotal(2));
            this._mtRepo.Insert(this._mt);
            this._mtRepo.Save();

            this._p0 = new Praxis(
                this._tId,
                this._pt,
                StatusReg.Active.Id,
                this._mt.Id
            );
            this._p1 = new Praxis(
                this._tId,
                this._pt,
                StatusReg.Active.Id,
                null,
                true
            );
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();

            this._pp = new PraxisParticipant(this._p0.Id, this._c0.Id);
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
            this._ppRepo.ResetNextIdValue();
            this._praxisRepo.ResetNextIdValue();
            this._charRepo.ResetNextIdValue();
            this._mtRepo.ResetNextIdValue();
        }

        [Test]
        public void TestRegisterHappy()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._ppReg.Register(null));
            this._ppReg.Register(this._pp);
            Assert.IsNotNull(this._ppRepo.GetById(this._pp.Id));
        }

        [Test]
        public void TestRegisterHappyAlt()
        {
            this._p0.MetaTaskId = null;
            this._praxisRepo.Update(this._p0);
            this._praxisRepo.Save();
            this._ppReg.Register(this._pp);
            Assert.IsNotNull(this._ppRepo.GetById(this._pp.Id));
        }

        [Test]
        public void TestRegisterInsufficientLevel()
        {
            var c = new Character(new Name("c"), new Id(9));
            this._charRepo.Insert(c);
            this._charRepo.Save();
            var pp = new PraxisParticipant(this._p0.Id, c.Id);
            Assert.Throws<ArgumentException>(()=>this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterTooManyPraxises()
        {
            this._eraReg.Register(new Era(
                new Name("few praxises allowed"),
                maxPraxises: 1
            ));

            this._ppReg.Register(this._pp);
            var pp = new PraxisParticipant(this._p1.Id, this._c0.Id);
            Assert.Throws<ArgumentException>(()=>
                this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterBadPraxis()
        {
            // Praxis DNE.
            var pp = new PraxisParticipant(new Id(320), this._c0.Id);
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));

            // Praxis that isn't active or in progress.
            var p = new Praxis(new Id(1), this._pt, StatusReg.Proposed.Id);
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();
            pp = new PraxisParticipant(p.Id, this._c0.Id);
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterBadCharacter()
        {
            var pp = new PraxisParticipant(this._p0.Id, new Id(23));
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterBadMetaTask()
        {
            // The character can't be a part of the sponsoring faction if they
            // don't have a faction.
            this._c0.FactionId = null;
            this._charRepo.Update(this._c0);
            this._charRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(this._pp));

            // The character is attempting to use a meta task that is not
            // sponsored by their faction.
            this._c0.FactionId = new Name("some other");
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(this._pp));
        } 

        [Test]
        public void TestRegisterDueling()
        {
            this._ppReg.Register(
                new PraxisParticipant(this._p1.Id, this._c0.Id));

            this._ppReg.Register(
                new PraxisParticipant(this._p1.Id, this._c1.Id));

            Assert.Throws<ArgumentException>(()=>this._ppReg.
                Register(new PraxisParticipant(this._p1.Id, this._c2.Id)));
        }

        [Test]
        public void TestRegisterTooManySubmissions()
        {
            var pp = new PraxisParticipant(this._p1.Id, this._c0.Id);
            this._ppReg.Register(pp);
            Assert.AreEqual(1, pp.Count);

            // Sad: exceeds submission count.
            pp = new PraxisParticipant(this._p1.Id, this._c0.Id);
            Assert.Throws<ArgumentException>(()=>
                this._ppReg.Register(pp));

            // Happy: make the character's faction have Reiterator, and then
            // they should be able to submit again.
            this._f.AbilityName = AbilityReg.Reiterator.Id;
            this._factionRepo.Update(this._f);
            this._factionRepo.Save();
            pp = new PraxisParticipant(this._p1.Id, this._c0.Id);
            this._ppReg.Register(pp);
            Assert.AreEqual(2, pp.Count);

            // Sad: Exceed Reiterator's buffer.
            pp = new PraxisParticipant(this._p1.Id, this._c0.Id);
            Assert.Throws<ArgumentException>(()=>
                this._ppReg.Register(pp));
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    null,
                    this._praxisRepo,
                    this._charRepo,
                    this._mtRepo,
                    this._taskRepo,
                    this._factionRepo,
                    this._eraReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    null,
                    this._charRepo,
                    this._mtRepo,
                    this._taskRepo,
                    this._factionRepo,
                    this._eraReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    null,
                    this._mtRepo,
                    this._taskRepo,
                    this._factionRepo,
                    this._eraReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    this._charRepo,
                    null,
                    this._taskRepo,
                    this._factionRepo,
                    this._eraReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    this._charRepo,
                    this._mtRepo,
                    null,
                    this._factionRepo,
                    this._eraReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    this._charRepo,
                    this._mtRepo,
                    this._taskRepo,
                    this._factionRepo,
                    null
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    this._charRepo,
                    this._mtRepo,
                    this._taskRepo,
                    null,
                    this._eraReg
                )
            );
            new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo,
                this._taskRepo,
                this._factionRepo,
                this._eraReg
            );
        }
    }

    public class DummyRAMPraxisParticipantRepo
        : RAMPraxisParticipantRepo
    {
        public DummyRAMPraxisParticipantRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }

    public class DummyRAMPraxisRepo
        : RAMPraxisRepo
    {
        public DummyRAMPraxisRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }

    public class DummyRAMMetaTaskRepo
        : RAMMetaTaskRepo
    {
        public DummyRAMMetaTaskRepo()
            : base()
        { }

        public void ResetNextIdValue() { _nextIdValue = 1; }
    }
}