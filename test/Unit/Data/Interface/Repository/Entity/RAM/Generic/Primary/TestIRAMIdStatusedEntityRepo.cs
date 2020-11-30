using System.Linq;
using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM.Generic.Primary
{
    [TestFixture]
    public class TestIRAMIdStatusedEntityRepo
    {
        private Name _statusId0;
        private Praxis _p0_0;
        private Praxis _p0_1;
        private Praxis _p1;
        private Praxis _p2;
        private TestRAMIdStatusedEntityRepo _repo;

        [SetUp]
        public void Setup()
        {
            var taskId = new Id(1000);
            var pt = new PointTotal(2);
            this._statusId0 = new Name("to be deleted");
            this._p0_0 = new Praxis(taskId, pt, this._statusId0);
            this._p0_1 = new Praxis(taskId, pt, this._statusId0);
            this._p1 = new Praxis(taskId, pt, new Name("other"));
            this._p2 = new Praxis(taskId, pt, new Name("alt"));
            this._repo = new TestRAMIdStatusedEntityRepo();
            this._repo.Insert(this._p0_0);
            this._repo.Insert(this._p0_1);
            this._repo.Insert(this._p1);
            this._repo.Insert(this._p2);
            this._repo.Save();
        }

        [Test]
        public void TestGetByStatusId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.GetByStatusId(null).Count());
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByStatusId(new Name("sdfsfa")).Count());

            var entities = this._repo.GetByStatusId(this._statusId0).ToList();
            Assert.AreEqual(2, entities.Count);
            Assert.AreEqual(this._p0_0.Id, entities[0].Id);
            Assert.AreEqual(this._p0_1.Id, entities[1].Id);
        }
    }

    public class TestRAMIdStatusedEntityRepo : IRAMIdStatusedEntityRepo<Praxis>
    {
        public TestRAMIdStatusedEntityRepo()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new Praxis(new Id(2), new PointTotal(2), new Name("x"));
            return a.GetUniqueRules().Count;
        }
    }
}