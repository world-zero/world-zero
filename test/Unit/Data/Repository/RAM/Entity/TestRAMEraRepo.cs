using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity
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

        [TearDown]
        public void TearDown()
        {
            this._repo.CleanAll();
        }

        [Test]
        public void TestGetActiveEraHappy()
        {
            Assert.IsNull(this._repo.GetActiveEra());

            Era e = new Era(new Name("The beginning"));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            e.EndDate = new PastDate(DateTime.UtcNow);
            this._repo.Update(e);
            Era r = new Era(new Name("The moon"));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.AreEqual(r.Id, this._repo.GetActiveEra().Id);
        }

        [Test]
        public void TestGetActiveEraBad()
        {
            Era e = new Era(new Name("The beginning"));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            Era r = new Era(new Name("The moon"));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.Throws<InvalidOperationException>(()=>this._repo.GetActiveEra());
        }
    }
}