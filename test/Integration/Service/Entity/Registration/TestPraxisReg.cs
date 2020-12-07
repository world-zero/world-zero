using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Registration.Primary;
using WorldZero.Service.Entity.Registration.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestPraxisReg
    {
        private PointTotal _pt;
        private IUnsafeFactionRepo _factionRepo;
        private DummyRAMPraxisParticipantRepo _ppRepo;
        private IUnsafeCharacterRepo _charRepo;
        private DummyRAMPraxisRepo _praxisRepo;
        private PraxisParticipantReg _ppReg;
        private ITaskRepo _taskRepo;
        private IUnsafeMetaTaskRepo _mtRepo;
        private IUnsafeStatusRepo _statusRepo;
        private PraxisReg _registration;
        private UnsafeStatus _status0;
        private UnsafeStatus _status1;
        private Task _t;
        private UnsafeCharacter _c;
        private UnsafePraxis _p;
        private UnsafeMetaTask _mt;
        private PraxisParticipant _pp;
        private List<PraxisParticipant> _pps;
        private UnsafeFaction _f;
        private IUnsafeEraRepo _eraRepo;
        private EraReg _eraReg;

        [SetUp]
        public void Setup()
        {
            this._pt = new PointTotal(2);
            this._factionRepo = new RAMUnsafeFactionRepo();
            this._eraRepo = new RAMUnsafeEraRepo();
            this._eraReg = new EraReg(this._eraRepo);
            this._ppRepo = new DummyRAMPraxisParticipantRepo();
            this._charRepo = new RAMUnsafeCharacterRepo();
            this._praxisRepo = new DummyRAMPraxisRepo();
            this._mtRepo = new RAMUnsafeMetaTaskRepo();
            this._taskRepo = new RAMTaskRepo();
            this._ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo,
                this._taskRepo,
                this._factionRepo,
                this._eraReg
            );
            this._statusRepo = new RAMUnsafeStatusRepo();
            this._registration = new PraxisReg(
                this._praxisRepo,
                this._taskRepo,
                this._mtRepo,
                this._statusRepo,
                this._ppReg
            );
            this._status0 = StatusReg.Active;
            this._status1 = StatusReg.Retired;
            this._statusRepo.Insert(this._status0);
            this._statusRepo.Insert(this._status1);
            this._statusRepo.Save();
            this._t = new Task(
                new Name("Legion of DIO"),
                this._status0.Id,
                "DIO's minions.",
                new PointTotal(5),
                new Level(3)
            );
            this._taskRepo.Insert(this._t);
            this._taskRepo.Save();

            this._f =
                new UnsafeFaction(new Name("Good"), new PastDate(DateTime.UtcNow));
            this._factionRepo.Insert(this._f);
            this._factionRepo.Save();
            this._c = new UnsafeCharacter(
                new Name("valid"),
                new Id(1),
                this._f.Id,
                eraPoints: new PointTotal(1000)
            );
            this._charRepo.Insert(this._c);
            this._charRepo.Save();

            this._mt = new UnsafeMetaTask(
                this._f.Id,
                StatusReg.Active.Id,
                "x",
                new PointTotal(2));
            this._mtRepo.Insert(this._mt);
            this._mtRepo.Save();

            this._p = new UnsafePraxis(
                this._t.Id,
                this._pt,
                StatusReg.Active.Id,
                this._mt.Id
            );
            this._pp = new PraxisParticipant(this._c.Id);
            this._pps = new List<PraxisParticipant>();
            this._pps.Add(this._pp);
            var c = new UnsafeCharacter(
                new Name("alt"),
                new Id(100),
                this._f.Id,
                eraPoints: new PointTotal(1000)
            );
            this._charRepo.Insert(c);
            this._charRepo.Save();
            this._pps.Add(new PraxisParticipant(c.Id));
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
        public void TestRegisterHappy()
        {
            var p = new UnsafePraxis(this._t.Id, this._pt,this._status0.Id);
            Assert.IsFalse(p.IsIdSet());
            Assert.IsFalse(this._pp.IsIdSet());
            this._registration.Register(p, this._pp);
            Assert.IsTrue(p.IsIdSet());
            Assert.IsTrue(p.IsIdSet());
            Assert.AreEqual(1, this._praxisRepo.Saved.Count);
            Assert.IsNotNull(this._praxisRepo.GetById(p.Id));
            Assert.AreEqual(1, this._ppRepo.Saved.Count);
            Assert.IsNotNull(this._ppRepo.GetById(this._pp.Id));
        }

        [Test]
        public void TestRegisterHappyWithMetaTask()
        {
            Assert.IsFalse(this._p.IsIdSet());
            this._registration.Register(this._p, this._pp);
            Assert.IsTrue(this._p.IsIdSet());
            Assert.IsNotNull(this._praxisRepo.GetById(this._p.Id));
        }

        [Test]
        public void TestRegisterNullPraxis()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._registration.Register(null, this._pp));
        }

        [Test]
        public void TestRegisterNullPraxisParticipant()
        {
            PraxisParticipant pp = null;
            Assert.Throws<ArgumentNullException>(
                ()=>this._registration.Register(null, pp));
        }

        [Test]
        public void TestRegisterBadPraxisStatus()
        {
            this._p.StatusId = StatusReg.Retired.Id;
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterAlreadyRegisteredParticipant()
        {
            this._pp.PraxisId = new Id(5);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterTaskDNE()
        {
            this._p.TaskId = new Id(1000);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterInactiveTask()
        {
            this._t.StatusId = StatusReg.Retired.Id;
            this._taskRepo.Update(this._t);
            this._taskRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterPPRegisterFails()
        {
            this._pp.CharacterId = new Id(10000);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
            Assert.AreEqual(0, this._praxisRepo.Saved.Count);
        }

        [Test]
        public void TestRegisterBadMetaTaskId()
        {
            this._p.MetaTaskId = new Id(100);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterInactiveMetaTask()
        {
            this._mt.StatusId = StatusReg.Retired.Id;
            this._mtRepo.Update(this._mt);
            this._mtRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterEmptyList()
        {
            var pps = new List<PraxisParticipant>();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, pps));
        }

        [Test]
        public void TestRegisterListHappy()
        {
            this._registration.Register(this._p, this._pps);
            Assert.AreEqual(2, this._ppRepo.Saved.Count);
            foreach (PraxisParticipant pp in this._pps)
                Assert.IsNotNull(this._ppRepo.GetById(pp.Id));
        }

        [Test]
        public void TestRegisterEnsureDiscardIsUsedOnPPRegFailure()
        {
            int expected = this._ppRepo.Saved.Count;

            var ppBad = new PraxisParticipant(new Id(666));
            this._pps.Add(ppBad);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pps));

            Assert.AreEqual(expected, this._ppRepo.Saved.Count);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    null,
                    this._taskRepo,
                    this._mtRepo,
                    this._statusRepo,
                    this._ppReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    null,
                    this._mtRepo,
                    this._statusRepo,
                    this._ppReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    null,
                    this._statusRepo,
                    this._ppReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    this._mtRepo,
                    null,
                    this._ppReg
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    this._mtRepo,
                    this._statusRepo,
                    null
                )
            );
            new PraxisReg(
                this._praxisRepo,
                this._taskRepo,
                this._mtRepo,
                this._statusRepo,
                this._ppReg
            );
        }
    }
}