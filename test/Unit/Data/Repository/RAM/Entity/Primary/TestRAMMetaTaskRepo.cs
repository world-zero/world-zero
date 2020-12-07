using System;
using System.Linq;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMMetaTaskRepo
    {
        private RAMMetaTaskRepo _mtRepo;
        private Name _f0;
        private Name _f1;
        private MetaTask _mt0_0;
        private MetaTask _mt0_1;
        private MetaTask _mt1_0;

        [SetUp]
        public void Setup()
        {
            this._mtRepo = new RAMMetaTaskRepo();
            var status = new Name("x");
            var pt = new PointTotal(2);
            this._f0 = new Name("first");
            this._f1 = new Name("second");
            this._mt0_0 = new MetaTask(this._f0, status, "x", pt);
            this._mt0_1 = new MetaTask(this._f0, status, "a", pt);
            this._mt1_0 = new MetaTask(this._f1, status, "y", pt);
            this._mtRepo.Insert(this._mt0_0);
            this._mtRepo.Insert(this._mt0_1);
            this._mtRepo.Insert(this._mt1_0);
            this._mtRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._mtRepo.CleanAll();
        }

        [Test]
        public void TestGetByFactionIdSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._mtRepo.GetByFactionId(null));
            Assert.Throws<ArgumentException>(()=>
                this._mtRepo.GetByFactionId(new Name("fake")));
        }

        [Test]
        public void TestGetByFactionId_f0()
        {
            var tasks = this._mtRepo.GetByFactionId(this._f0).ToList();
            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual(this._mt0_0.Id, tasks[0].Id);
            Assert.AreEqual(this._mt0_1.Id, tasks[1].Id);
        }

        [Test]
        public void TestGetByFactionId_f1()
        {
            var tasks = this._mtRepo.GetByFactionId(this._f1).ToList();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual(this._mt1_0.Id, tasks[0].Id);
        }
    }
}