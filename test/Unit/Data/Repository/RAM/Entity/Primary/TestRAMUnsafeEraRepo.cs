using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMUnsafeEraRepo
    {
        private RAMUnsafeEraRepo _repo;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMUnsafeEraRepo();
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

            UnsafeEra e = new UnsafeEra(new Name("The beginning"));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            e.EndDate = new PastDate(DateTime.UtcNow);
            this._repo.Update(e);
            UnsafeEra r = new UnsafeEra(new Name("The moon"));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.AreEqual(r.Id, this._repo.GetActiveEra().Id);
        }

        [Test]
        public void TestGetActiveEraBad()
        {
            UnsafeEra e = new UnsafeEra(new Name("The beginning"));
            this._repo.Insert(e);
            this._repo.Save();
            Assert.AreEqual(e.Id, this._repo.GetActiveEra().Id);

            UnsafeEra r = new UnsafeEra(new Name("The moon"));
            this._repo.Insert(r);
            this._repo.Save();
            Assert.Throws<InvalidOperationException>(()=>this._repo.GetActiveEra());
        }
    }
}