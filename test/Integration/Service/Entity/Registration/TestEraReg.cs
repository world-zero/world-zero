using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestEraReg
    {
        private DummyRAMEraRepo _eraRepo;
        private EraReg _eraReg;
        private Era _e;

        [SetUp]
        public void Setup()
        {
            this._e = new Era(new Name("master"));
            this._eraRepo = new DummyRAMEraRepo();
            this._eraReg = new EraReg(this._eraRepo);
            this._eraReg.Register(this._e);
            Assert.AreEqual(2, this._eraRepo.Saved.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (this._eraRepo.IsTransactionActive())
            {
                this._eraRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._eraRepo.CleanAll();
        }

        [OneTimeSetUp]
        public void TestConstructor()
        {
            var eraRepo = new DummyRAMEraRepo();
            var eraReg = new EraReg(eraRepo);
            Assert.AreEqual(1, eraRepo.Saved.Count);

            eraReg = new EraReg(eraRepo);
            Assert.AreEqual(1, eraRepo.Saved.Count);
        }

        [Test]
        public void TestGetActiveEraHappy()
        {
            var e = new Era(new Name("test"));
            this._eraReg.Register(e);
            var actual = this._eraReg.GetActiveEra();
            Assert.AreEqual(e.Id, actual.Id);
        }

        [Test]
        public void TestGetActiveEraSad()
        {
            this._eraRepo.Delete(this._e.Id);
            this._eraRepo.Save();
            Assert.Throws<InvalidOperationException>(()=>
                this._eraReg.GetActiveEra());
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>this._eraReg.Register(null));

            PastDate badExpected = new PastDate(new DateTime(2000, 1, 1));
            Era e = new Era(
                new Name("first"),
                null,
                10,
                1,
                2,
                badExpected,
                new PastDate(DateTime.UtcNow)
            );
            Era result = this._eraReg.Register(e);
            Assert.AreEqual(e.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);

            Era r = new Era(new Name("second"), startDate: badExpected);
            result = this._eraReg.Register(r);
            Assert.AreEqual(r.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);
            Era oldE = this._eraRepo.GetById(e.Id);
            Assert.IsNotNull(oldE.EndDate);
            Assert.AreEqual(oldE.EndDate, r.StartDate);

            Era a = new Era(new Name("third"), startDate: badExpected);
            result = this._eraReg.Register(a);
            Assert.AreEqual(a.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);
            Era oldR = this._eraRepo.GetById(r.Id);
            Assert.IsNotNull(oldR.EndDate);
            Assert.AreEqual(oldR.EndDate, a.StartDate);
        }
    }

    public class DummyRAMEraRepo
        : RAMEraRepo
    {
        public DummyRAMEraRepo()
            : base()
        { }

        public Dictionary<object, object> Saved
        { get { return this._saved; } }
    }
}