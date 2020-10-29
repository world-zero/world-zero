using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Registration.Entity;
using WorldZero.Service.Registration.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity.Relation
{
    [TestFixture]
    public class TestPraxisParticipantReg
    {
        private DummyRAMPraxisParticipantRepo _ppRepo;
        private DummyRAMPraxisRepo _praxisRepo;
        private DummyRAMCharacterRepo _charRepo;
        private DummyRAMMetaTaskRepo _mtRepo;
        private PraxisParticipantReg _ppReg;
        private Character _c;
        private Praxis _p;
        private MetaTask _mt;
        private PraxisParticipant _pp;
        private Faction _f;

        [SetUp]
        public void Setup()
        {
            this._ppRepo = new DummyRAMPraxisParticipantRepo();
            this._praxisRepo = new DummyRAMPraxisRepo();
            this._charRepo = new DummyRAMCharacterRepo();
            this._mtRepo = new DummyRAMMetaTaskRepo();
            this._ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo
            );

            this._f =
                new Faction(new Name("Good"), new PastDate(DateTime.UtcNow));
            this._c = new Character(new Name("valid"), new Id(1), this._f.Id);
            this._charRepo.Insert(this._c);
            this._charRepo.Save();

            this._mt = new MetaTask(this._f.Id, StatusReg.Active.Id, "x", 2);
            this._mtRepo.Insert(this._mt);
            this._mtRepo.Save();

            this._p = new Praxis(new Id(1), StatusReg.Active.Id, this._mt.Id);
            this._praxisRepo.Insert(this._p);
            this._praxisRepo.Save();

            this._pp = new PraxisParticipant(this._p.Id, this._c.Id);
        }

        [TearDown]
        public void TearDown()
        {
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
            this._p.MetaTaskId = null;
            this._praxisRepo.Update(this._p);
            this._praxisRepo.Save();
            this._ppReg.Register(this._pp);
            Assert.IsNotNull(this._ppRepo.GetById(this._pp.Id));
        }

        [Test]
        public void TestRegisterBadPraxis()
        {
            // Praxis DNE.
            var pp = new PraxisParticipant(new Id(320), this._c.Id);
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));

            // Praxis that isn't active or in progress.
            var p = new Praxis(new Id(1), StatusReg.Proposed.Id);
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();
            pp = new PraxisParticipant(p.Id, this._c.Id);
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterBadCharacter()
        {
            var pp = new PraxisParticipant(this._p.Id, new Id(23));
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(pp));
        }

        [Test]
        public void TestRegisterBadMetaTask()
        {
            // The character can't be a part of the sponsoring faction if they
            // don't have a faction.
            this._c.FactionId = null;
            this._charRepo.Update(this._c);
            this._charRepo.Save();
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(this._pp));

            // The character is attempting to use a meta task that is not
            // sponsored by their faction.
            this._c.FactionId = new Name("some other");
            Assert.Throws<ArgumentException>(
                ()=>this._ppReg.Register(this._pp));
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
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
                    this._mtRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    null,
                    this._charRepo,
                    this._mtRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    null,
                    this._mtRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new PraxisParticipantReg(
                    this._ppRepo,
                    this._praxisRepo,
                    this._charRepo,
                    null
                )
            );
            new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo
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