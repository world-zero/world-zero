using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.RAM.Entity.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestMetaTaskReg
    {
        private IMetaTaskRepo _metaTaskRepo;
        private IFactionRepo _factionRepo;
        private IStatusRepo _statusRepo;
        private MetaTaskReg _registration;
        private Status _status0;
        private Status _status1;
        private Faction _faction0;

        [SetUp]
        public void Setup()
        {
            this._metaTaskRepo = new RAMMetaTaskRepo();
            this._factionRepo = new RAMFactionRepo();
            this._statusRepo = new RAMStatusRepo();
            this._registration = new MetaTaskReg(
                this._metaTaskRepo,
                this._factionRepo,
                this._statusRepo
            );
            this._status0 = new Status(new Name("INVALID"));
            this._status1 = new Status(new Name("VALID"));
            this._statusRepo.Insert(this._status0);
            this._statusRepo.Insert(this._status1);
            this._statusRepo.Save();
            this._faction0 = new Faction(
                new Name("DIO's Minions"),
                new PastDate(DateTime.UtcNow)
            );
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Save();
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
            var mt = new MetaTask(
                this._faction0.Id,
                StatusReg.Active.Id,
                "something",
                new PointTotal(33.4));
            Assert.IsFalse(mt.IsIdSet());
            this._registration.Register(mt);
            Assert.IsTrue(mt.IsIdSet());
            Assert.IsNotNull(this._metaTaskRepo.GetById(mt.Id));
        }

        [Test]
        public void TestRegisterSad()
        {
            var mt = new MetaTask(
                new Name("fake"),
                new Name("extra fake"),
                "something",
                new PointTotal(33.4));
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );

            mt = new MetaTask(
                new Name("fake"),
                this._status0.Id,
                "something",
                new PointTotal(33.4));
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );

            mt = new MetaTask(
                this._faction0.Id,
                new Name("ya basic"),
                "something",
                new PointTotal(33.4));
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new MetaTaskReg(
                    null,
                    null,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new MetaTaskReg(
                    this._metaTaskRepo,
                    null,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new MetaTaskReg(
                    this._metaTaskRepo,
                    this._factionRepo,
                    null)
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new MetaTaskReg(
                    this._metaTaskRepo,
                    null,
                    this._statusRepo)
            );
            new MetaTaskReg(
                this._metaTaskRepo,
                this._factionRepo,
                this._statusRepo
            );
        }
    }
}