using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.Entity.RAM;
using WorldZero.Service.Registration.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity
{
    [TestFixture]
    public class TestPraxisRegistration
    {
        private IPraxisRepo _praxisRepo;
        private ITaskRepo _taskRepo;
        private IStatusRepo _statusRepo;
        private PraxisRegistration _registration;
        private Status _status0;
        private Status _status1;
        private Task _task0;

        [SetUp]
        public void Setup()
        {
            this._praxisRepo = new RAMPraxisRepo();
            this._taskRepo = new RAMTaskRepo();
            this._statusRepo = new RAMStatusRepo();
            this._registration = new PraxisRegistration(
                this._praxisRepo,
                this._taskRepo,
                this._statusRepo
            );
            this._status0 = new Status(new Name("Active"));
            this._status1 = new Status(new Name("Retired"));
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

        [Test]
        public void TestRegisterHappy()
        {
            // TODO: run this through the debugger and see why it's failing
            var p = new Praxis(this._task0.Id, this._status0.Id);
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
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisRegistration(
                    null,
                    null,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisRegistration(
                    this._praxisRepo,
                    null,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisRegistration(
                    this._praxisRepo,
                    this._taskRepo,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisRegistration(
                    this._praxisRepo,
                    null,
                    this._statusRepo)
            );
            new PraxisRegistration(
                this._praxisRepo,
                this._taskRepo,
                this._statusRepo
            );
        }
    }
}