using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Update.Primary;
using WorldZero.Service.Constant.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Update.Primary
{
    [TestFixture]
    public class TestPraxisUpdate
    {
        private RAMPraxisRepo _repo;
        private RAMPraxisParticipantRepo _ppRepo;
        private RAMMetaTaskRepo _mtRepo;
        private RAMStatusRepo _statusRepo;
        private PraxisUpdate _update;
        private RAMFactionRepo _factionRepo;
        private RAMLocationRepo _locationRepo;
        private RAMCharacterRepo _charRepo;
        private CharacterUpdate _charUpdate;
        private IPraxis _p0;
        private IMetaTask _mt0;
        private IMetaTask _mt1;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._statusRepo = new RAMStatusRepo();
            this._factionRepo = new RAMFactionRepo();
            this._locationRepo = new RAMLocationRepo();
            this._charRepo = new RAMCharacterRepo();
            this._charUpdate = new CharacterUpdate(
                this._charRepo,
                this._factionRepo,
                this._locationRepo
            );
            this._update = new PraxisUpdate(
                this._repo,
                this._ppRepo,
                this._statusRepo,
                this._mtRepo,
                this._charRepo,
                this._charUpdate
            );
            var pt = new PointTotal(2);
            this._p0 =
                new UnsafePraxis(new Id(232), pt, ConstantStatuses.Active.Id);
            this._repo.Insert(this._p0);
            this._repo.Save();
            this._mt0 = new UnsafeMetaTask(
                new Name("f0"),
                ConstantStatuses.Active.Id,
                "dsf",
                pt
            );
            this._mt1 = new UnsafeMetaTask(
                new Name("f1"),
                ConstantStatuses.Active.Id,
                "dsdff",
                pt
            );
            this._mtRepo.Insert(this._mt0);
            this._mtRepo.Insert(this._mt1);
            this._mtRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestAmendAreDuelingSequencialActions()
        {
            var pp = new UnsafePraxisParticipant(this._p0.Id, new Id(1000));
            this._ppRepo.Insert(pp);
            this._ppRepo.Save();
            Assert.AreEqual(1,
                this._ppRepo.GetParticipantCountViaPraxisId(this._p0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAreDueling(this._p0, true));
            Assert.IsFalse(this._repo.GetById(this._p0.Id).AreDueling);

            pp = new UnsafePraxisParticipant(this._p0.Id, new Id(2000));
            this._ppRepo.Insert(pp);
            this._ppRepo.Save();
            Assert.AreEqual(2,
                this._ppRepo.GetParticipantCountViaPraxisId(this._p0.Id));
            this._update.AmendAreDueling(this._p0, true);
            Assert.IsTrue(this._repo.GetById(this._p0.Id).AreDueling);
            this._update.AmendAreDueling(this._p0, false);
            Assert.IsFalse(this._repo.GetById(this._p0.Id).AreDueling);

            pp = new UnsafePraxisParticipant(this._p0.Id, new Id(3000));
            this._ppRepo.Insert(pp);
            this._ppRepo.Save();
            Assert.AreEqual(3,
                this._ppRepo.GetParticipantCountViaPraxisId(this._p0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAreDueling(this._p0, true));
            Assert.IsFalse(this._repo.GetById(this._p0.Id).AreDueling);
        }

        [Test]
        public void TestAmendMetaTaskSad_IPraxis_IMetaTask()
        {
            IPraxis c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendMetaTask(c, this._mt0));

            UnsafeMetaTask mt = new UnsafeMetaTask(
                new Name("bad mt"),
                ConstantStatuses.Proposed.Id,
                "baaadd",
                new PointTotal(234)
            );
            Assert.IsNull(this._p0.MetaTaskId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendMetaTask(this._p0, mt));
            Assert.IsNull(this._repo.GetById(this._p0.Id).MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskHappy_IPraxis_IMetaTask()
        {
            Assert.IsNull(this._p0.MetaTaskId);

            this._update.AmendMetaTask(this._p0, this._mt0);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt0.Id);

            this._update.AmendMetaTask(this._p0, this._mt1);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId,this._mt1.Id);

            IMetaTask l = null;
            this._update.AmendMetaTask(this._p0, l);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.IsNull(this._p0.MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskSad_IPraxis_MetaTaskId()
        {
            IPraxis c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendMetaTask(c, this._mt0.Id));

            Id a = new Id(3242);
            Assert.IsNull(this._p0.MetaTaskId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendMetaTask(this._p0, a));
            Assert.IsNull(this._repo.GetById(this._p0.Id).MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskHappy_IPraxis_MetaTaskId()
        {
            Assert.IsNull(this._p0.MetaTaskId);

            this._update.AmendMetaTask(this._p0, this._mt0.Id);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt0.Id);

            this._update.AmendMetaTask(this._p0, this._mt1.Id);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt1.Id);

            Id a = null;
            this._update.AmendMetaTask(this._p0, a);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.IsNull(this._p0.MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskSad_PraxisId_IMetaTask()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendMetaTask(c, this._mt0));


            UnsafeMetaTask mt = new UnsafeMetaTask(
                new Name("bad mt"),
                ConstantStatuses.Proposed.Id,
                "baaadd",
                new PointTotal(234)
           );
            Assert.IsNull(this._p0.MetaTaskId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendMetaTask(this._p0.Id, mt));
            Assert.IsNull(this._repo.GetById(this._p0.Id).MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskHappy_PraxisId_IMetaTask()
        {
            Assert.IsNull(this._p0.MetaTaskId);

            this._update.AmendMetaTask(this._p0.Id, this._mt0);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt0.Id);

            this._update.AmendMetaTask(this._p0.Id, this._mt1);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt1.Id);

            IMetaTask a = null;
            this._update.AmendMetaTask(this._p0.Id, a);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.IsNull(this._p0.MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskSad_PraxisId_MetaTaskId()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendMetaTask(c, this._mt0.Id));

            Id a = new Id(324342);
            Assert.IsNull(this._p0.MetaTaskId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendMetaTask(this._p0.Id, a));
            Assert.IsNull(this._repo.GetById(this._p0.Id).MetaTaskId);
        }

        [Test]
        public void TestAmendMetaTaskHappy_PraxisId_MetaTaskId()
        {
            Assert.IsNull(this._p0.MetaTaskId);

            this._update.AmendMetaTask(this._p0.Id, this._mt0.Id);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt0.Id);

            this._update.AmendMetaTask(this._p0.Id, this._mt1.Id);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.AreEqual(this._p0.MetaTaskId, this._mt1.Id);

            Id a = null;
            this._update.AmendMetaTask(this._p0.Id, a);
            this._p0 = this._repo.GetById(this._p0.Id);
            Assert.IsNull(this._p0.MetaTaskId);
        }

        // --------------------------------------------------------------------

        [Test]
        public void TestAmendStatusWithMT()
        {
            var c0pt = new PointTotal(20000);
            var c1pt = new PointTotal(10000);
            var c0 =
                new UnsafeCharacter(new Name("0"), new Id(1), eraPoints: c0pt);
            var c1 =
                new UnsafeCharacter(new Name("1"), new Id(2), eraPoints: c1pt);
            this._charRepo.Insert(c0);
            this._charRepo.Insert(c1);
            this._charRepo.Save();
            var pp0 = new UnsafePraxisParticipant(this._p0.Id, c0.Id);
            var pp1 = new UnsafePraxisParticipant(this._p0.Id, c1.Id);
            this._ppRepo.Insert(pp0);
            this._ppRepo.Insert(pp1);
            this._ppRepo.Save();

            Assert.AreEqual(ConstantStatuses.Active.Id, this._p0.StatusId);
            ((UnsafePraxis) this._p0).MetaTaskId = this._mt0.Id;
            this._repo.Update(this._p0);
            this._repo.Save();
            ((UnsafeMetaTask) this._mt0).IsFlatBonus = true;
            this._mtRepo.Update(this._mt0);
            this._mtRepo.Save();

            this._update.AmendStatus(this._p0.Id, ConstantStatuses.Retired);
            Assert.AreEqual(
                    ConstantStatuses.Retired.Id.Get,
                    this._repo.GetById(this._p0.Id).StatusId.Get);
            Assert.AreEqual(
                PointTotal.ApplyBonus(
                    c0pt, this._mt0.Bonus, this._mt0.IsFlatBonus
                ).Get + this._p0.Points.Get,
                this._charRepo.GetById(c0.Id).EraPoints.Get
            );
            Assert.AreEqual(
                PointTotal.ApplyBonus(
                    c1pt, this._mt1.Bonus, this._mt0.IsFlatBonus).Get
                    + this._p0.Points.Get,
                this._charRepo.GetById(c1.Id).EraPoints.Get
            );
        }

        [Test]
        public void TestAmendStatusWithoutMT()
        {
            var c0pt = new PointTotal(20000);
            var c1pt = new PointTotal(10000);
            var c0 =
                new UnsafeCharacter(new Name("0"), new Id(1), eraPoints: c0pt);
            var c1 =
                new UnsafeCharacter(new Name("1"), new Id(2), eraPoints: c1pt);
            this._charRepo.Insert(c0);
            this._charRepo.Insert(c1);
            this._charRepo.Save();
            var pp0 = new UnsafePraxisParticipant(this._p0.Id, c0.Id);
            var pp1 = new UnsafePraxisParticipant(this._p0.Id, c1.Id);
            this._ppRepo.Insert(pp0);
            this._ppRepo.Insert(pp1);
            this._ppRepo.Save();

            Assert.AreEqual(ConstantStatuses.Active.Id, this._p0.StatusId);
            this._update.AmendStatus(this._p0.Id, ConstantStatuses.Retired.Id);
            Assert.AreEqual(
                    ConstantStatuses.Retired.Id.Get,
                    this._repo.GetById(this._p0.Id).StatusId.Get);
            Assert.AreEqual(
                c0pt.Get + this._p0.Points.Get,
                this._charRepo.GetById(c0.Id).EraPoints.Get
            );
            Assert.AreEqual(
                c1pt.Get + this._p0.Points.Get,
                this._charRepo.GetById(c1.Id).EraPoints.Get
            );
        }
    }
}