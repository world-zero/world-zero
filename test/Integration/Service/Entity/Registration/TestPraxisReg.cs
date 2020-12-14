using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using WorldZero.Service.Entity.Registration.Relation;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestPraxisReg
    {
        private PointTotal _pt;
        private IFactionRepo _factionRepo;
        private DummyRAMPraxisParticipantRepo _ppRepo;
        private ICharacterRepo _charRepo;
        private DummyRAMPraxisRepo _praxisRepo;
        private PraxisParticipantReg _ppReg;
        private ITaskRepo _taskRepo;
        private IMetaTaskRepo _mtRepo;
        private IStatusRepo _statusRepo;
        private PraxisReg _registration;
        private UnsafeStatus _status0;
        private UnsafeStatus _status1;
        private UnsafeTask _t;
        private UnsafeCharacter _c;
        private UnsafePraxis _p;
        private UnsafeMetaTask _mt;
        private IPraxisParticipant _pp;
        private List<IPraxisParticipant> _pps;
        private UnsafeFaction _f;
        private IEraRepo _eraRepo;
        private EraReg _eraReg;

        [SetUp]
        public void Setup()
        {
            this._pt = new PointTotal(2);
            this._factionRepo = new RAMFactionRepo();
            this._eraRepo = new RAMEraRepo();
            this._eraReg = new EraReg(this._eraRepo);
            this._ppRepo = new DummyRAMPraxisParticipantRepo();
            this._charRepo = new RAMCharacterRepo();
            this._praxisRepo = new DummyRAMPraxisRepo();
            this._mtRepo = new RAMMetaTaskRepo();
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
            this._statusRepo = new RAMStatusRepo();
            this._registration = new PraxisReg(
                this._praxisRepo,
                this._taskRepo,
                this._mtRepo,
                this._statusRepo,
                this._ppReg
            );
            this._status0 = IStatusReg.Active;
            this._status1 = IStatusReg.Retired;
            this._statusRepo.Insert(this._status0);
            this._statusRepo.Insert(this._status1);
            this._statusRepo.Save();
            this._t = new UnsafeTask(
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
                IStatusReg.Active.Id,
                "x",
                new PointTotal(2));
            this._mtRepo.Insert(this._mt);
            this._mtRepo.Save();

            this._p = new UnsafePraxis(
                this._t.Id,
                this._pt,
                IStatusReg.Active.Id,
                this._mt.Id
            );
            this._pp = new UnsafePraxisParticipant(this._c.Id);
            this._pps = new List<IPraxisParticipant>();
            this._pps.Add(this._pp);
            var c = new UnsafeCharacter(
                new Name("alt"),
                new Id(100),
                this._f.Id,
                eraPoints: new PointTotal(1000)
            );
            this._charRepo.Insert(c);
            this._charRepo.Save();
            this._pps.Add(new UnsafePraxisParticipant(c.Id));
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
            UnsafePraxisParticipant pp = null;
            Assert.Throws<ArgumentNullException>(
                ()=>this._registration.Register(null, pp));
        }

        [Test]
        public void TestRegisterBadPraxisStatus()
        {
            this._p.StatusId = IStatusReg.Retired.Id;
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterAlreadyRegisteredParticipant()
        {
            ((UnsafePraxisParticipant) this._pp).PraxisId = new Id(5);
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
            this._t.StatusId = IStatusReg.Retired.Id;
            this._taskRepo.Update(this._t);
            this._taskRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterPPRegisterFails()
        {
            ((UnsafePraxisParticipant) this._pp).CharacterId = new Id(10000);
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
            this._mt.StatusId = IStatusReg.Retired.Id;
            this._mtRepo.Update(this._mt);
            this._mtRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, this._pp));
        }

        [Test]
        public void TestRegisterEmptyList()
        {
            var pps = new List<IPraxisParticipant>();
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(this._p, pps));
        }

        [Test]
        public void TestRegisterListHappy()
        {
            this._registration.Register(this._p, this._pps);
            Assert.AreEqual(2, this._ppRepo.Saved.Count);
            foreach (UnsafePraxisParticipant pp in this._pps)
                Assert.IsNotNull(this._ppRepo.GetById(pp.Id));
        }

        [Test]
        public void TestRegisterEnsureDiscardIsUsedOnPPRegFailure()
        {
            int expected = this._ppRepo.Saved.Count;

            var ppBad = new UnsafePraxisParticipant(new Id(666));
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