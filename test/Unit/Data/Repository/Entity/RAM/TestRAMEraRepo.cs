using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Repository.Entity.RAM;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.Entity.RAM
{
    [TestFixture]
    public class TestRAMEraRepo
    {
        private RAMEraRepo _repo;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMEraRepo();
        }

        [Test]
        public void TestGetActiveEraHappy()
        {
            Assert.IsNull(this._repo.GetActiveEra());

            Era e = new Era(new Name("The beginning"), new PastDate(DateTime.UtcNow));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            e.EndDate = new PastDate(DateTime.UtcNow);
            this._repo.Update(e);
            Era r = new Era(new Name("The moon"), new PastDate(DateTime.UtcNow));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.AreEqual(r.Id, this._repo.GetActiveEra().Id);
        }

        [Test]
        public void TestGetActiveEraBad()
        {
            Era e = new Era(new Name("The beginning"), new PastDate(DateTime.UtcNow));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            Era r = new Era(new Name("The moon"), new PastDate(DateTime.UtcNow));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.Throws<InvalidOperationException>(()=>this._repo.GetActiveEra());
        }
    }
}