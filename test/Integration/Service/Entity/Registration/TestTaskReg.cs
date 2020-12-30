using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestTaskReg
    {
        private ITaskRepo _taskRepo;
        private TaskReg _taskReg;
        private IFactionRepo _factionRepo;
        private UnsafeFaction _faction0;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMFactionRepo();
            this._faction0 = new UnsafeFaction(
                new Name("The JoJos"),
                new PastDate(DateTime.UtcNow)
            );
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Save();

            this._taskRepo = new RAMTaskRepo();
            this._taskReg = new TaskReg(this._taskRepo, this._factionRepo);
        }

        [TearDown]
        public void TearDown()
        {
            if (this._factionRepo.IsTransactionActive())
            {
                this._factionRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._factionRepo.CleanAll();
        }

        [Test]
        public void TestRegisterHappy()
        {
            var t = new UnsafeTask(
                this._faction0.Id,
                new Name("Active"),
                "sdf",
                new PointTotal(2),
                new Level(2)
            );
            this._taskReg.Register(t);
            Assert.IsTrue(t.IsIdSet());
        }

        [Test]
        public void TestRegisterSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._taskReg.Register(null));
            Assert.Throws<ArgumentException>(()=>this._taskReg.Register(
                new UnsafeTask(
                    new Name("fadffsd"),
                    new Name("dsf"),
                    "sdf",
                    new PointTotal(2),
                    new Level(2)
                )));
        }

        [Test]
        public void TestConstructor()
        {
            new TaskReg(
                this._taskRepo,
                this._factionRepo
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new TaskReg(
                    null,
                    this._factionRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new TaskReg(
                    this._taskRepo,
                    null
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new TaskReg(
                    null,
                    null
                )
            );
        }
    }
}