using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Service.Registration.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity
{
    [TestFixture]
    public class TestEraReg
    {
        private IEraRepo _repo;
        private EraReg _eraReg;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMEraRepo();
            this._eraReg = new EraReg(this._repo);
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>this._eraReg.Register(null));

            PastDate badExpected = new PastDate(new DateTime(2000, 1, 1));
            Era e = new Era(new Name("first"), badExpected, new PastDate(DateTime.UtcNow));
            Era result = this._eraReg.Register(e);
            Assert.AreEqual(e.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);

            Era r = new Era(new Name("second"), badExpected);
            result = this._eraReg.Register(r);
            Assert.AreEqual(r.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);
            Era oldE = this._repo.GetById(e.Id);
            Assert.IsNotNull(oldE.EndDate);
            Assert.AreEqual(oldE.EndDate, r.StartDate);

            Era a = new Era(new Name("third"), badExpected);
            result = this._eraReg.Register(a);
            Assert.AreEqual(a.Id, result.Id);
            Assert.AreNotEqual(badExpected, result.StartDate);
            Assert.IsNull(result.EndDate);
            Era oldR = this._repo.GetById(r.Id);
            Assert.IsNotNull(oldR.EndDate);
            Assert.AreEqual(oldR.EndDate, a.StartDate);
        }
    }
}