using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Registration.Entity;
using WorldZero.Service.Registration.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity
{
    [TestFixture]
    public class TestPraxisReg
    {
        private IPraxisParticipantRepo _ppRepo;
        private ICharacterRepo _charRepo;
        private IPraxisRepo _praxisRepo;
        private PraxisParticipantReg _ppReg;
        private ITaskRepo _taskRepo;
        private IMetaTaskRepo _mtRepo;
        private IStatusRepo _statusRepo;
        private PraxisReg _registration;
        private Status _status0;
        private Status _status1;
        private Task _task0;

        [SetUp]
        public void Setup()
        {
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._charRepo = new RAMCharacterRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo
            );
            this._taskRepo = new RAMTaskRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._statusRepo = new RAMStatusRepo();
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
            this._task0 = new Task(
                new Name("Legion of DIO"),
                this._status0.Id,
                "DIO's minions.",
                new PointTotal(5),
                new Level(3)
            );
            this._taskRepo.Insert(this._task0);
            this._taskRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._praxisRepo.CleanAll();
        }

        [Test]
        public void TestRegisterHappyWith()
        {
            var p = new Praxis(this._task0.Id, this._status0.Id);
            Assert.IsFalse(p.IsIdSet());
            this._registration.Register(p);
            Assert.IsTrue(p.IsIdSet());
            Assert.IsNotNull(this._praxisRepo.GetById(p.Id));
        }

        [Test]
        public void TestRegisterHappyWithMetaTask()
        {
            var mt =
                new MetaTask(new Name("x"), StatusReg.Active.Id, "x", 2);
            this._mtRepo.Insert(mt);
            this._mtRepo.Save();
            var p = new Praxis(
                this._task0.Id,
                StatusReg.InProgress.Id,
                mt.Id
            );
            Assert.IsFalse(p.IsIdSet());
            this._registration.Register(p);
            Assert.IsTrue(p.IsIdSet());
            Assert.IsNotNull(this._praxisRepo.GetById(p.Id));
        }

        [Test]
        public void TestRegisterSad()
        {
            var p = new Praxis(new Id(9001), new Name("Fake"));
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(p)
            );

            p = new Praxis(new Id(9001), this._status0.Id);
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(p)
            );

            p = new Praxis(this._task0.Id, new Name("ya basic"));
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(p)
            );

            var mt = new MetaTask(new Name("x"), new Name("faaake"), "x", 2);
            this._mtRepo.Insert(mt);
            this._mtRepo.Save();
            p = new Praxis(
                this._task0.Id,
                StatusReg.Active.Id,
                mt.Id
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(p)
            );
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
                    this._ppReg)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    null,
                    this._mtRepo,
                    this._statusRepo,
                    this._ppReg)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    null,
                    this._statusRepo,
                    this._ppReg)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    this._mtRepo,
                    null,
                    this._ppReg)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisReg(
                    this._praxisRepo,
                    this._taskRepo,
                    this._mtRepo,
                    this._statusRepo,
                    null)
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