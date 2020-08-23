using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.Entity.RAM;
using WorldZero.Service.Entity.Registration;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Service.Entity.Registration
{
    [TestFixture]
    public class TestMetaTaskRegistration
    {
        private IMetaTaskRepo _metaTaskRepo;
        private IFactionRepo _factionRepo;
        private IStatusRepo _statusRepo;
        private MetaTaskRegistration _registration;
        private Status _status0;
        private Status _status1;
        private Faction _faction0;

        [SetUp]
        public void Setup()
        {
            this._metaTaskRepo = new RAMMetaTaskRepo();
            this._factionRepo = new RAMFactionRepo();
            this._statusRepo = new RAMStatusRepo();
            this._registration = new MetaTaskRegistration(
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

        [Test]
        public void TestRegisterHappy()
        {
            var mt = new MetaTask(
                this._faction0.Id, this._status1.Id, "something", 33.4
            );
            Assert.IsFalse(mt.IsIdSet());
            this._registration.Register(mt);
            Assert.IsTrue(mt.IsIdSet());
            Assert.IsNotNull(this._metaTaskRepo.GetById(mt.Id));
        }

        [Test]
        public void TestRegisterSad()
        {
            var mt = new MetaTask(
                new Name("fake"), new Name("extra fake"), "something", 33.4
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );

            mt = new MetaTask(
                new Name("fake"), this._status0.Id, "something", 33.4
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );

            mt = new MetaTask(
                this._faction0.Id, new Name("ya basic"), "something", 33.4
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(mt)
            );
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentException>(
                ()=>new MetaTaskRegistration(
                    null,
                    null,
                    null)
            );
            Assert.Throws<ArgumentException>(
                ()=>new MetaTaskRegistration(
                    this._metaTaskRepo,
                    null,
                    null)
            );
            Assert.Throws<ArgumentException>(
                ()=>new MetaTaskRegistration(
                    this._metaTaskRepo,
                    this._factionRepo,
                    null)
            );
            Assert.Throws<ArgumentException>(
                ()=>new MetaTaskRegistration(
                    this._metaTaskRepo,
                    null,
                    this._statusRepo)
            );
            new MetaTaskRegistration(
                this._metaTaskRepo,
                this._factionRepo,
                this._statusRepo
            );
        }
    }
}