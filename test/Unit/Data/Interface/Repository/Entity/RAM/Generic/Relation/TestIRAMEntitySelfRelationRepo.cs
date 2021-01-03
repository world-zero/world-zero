using System;
using System.Linq;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using NUnit.Framework;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity.Generic.Relation
{
    [TestFixture]
    public class TestIEntitySelfRelationRepo
    {
        private Id _id0;
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Foe _f0;
        private Foe _f1;
        private Foe _f2;
        private RAMFoeRepo _repo;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(1);
            this._id1 = new Id(2);
            this._id2 = new Id(3);
            this._id3 = new Id(4);

            this._f0 = new Foe(this._id0, this._id1);
            this._f1 = new Foe(this._id2, this._id0);
            this._f2 = new Foe(this._id1, this._id3);

            this._repo = new RAMFoeRepo();
            this._repo.Insert(this._f0);
            this._repo.Insert(this._f1);
            this._repo.Insert(this._f2);
            this._repo.Save();
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
        public void TestDeleteByRelatedIdFirst()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.DeleteByRelatedId(null));

            this._repo.DeleteByRelatedId(this._id0);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f1.Id));
            this._repo.GetById(this._f2.Id);
        }

        [Test]
        public void TestDeleteByRelatedIdSecond()
        {
            this._repo.DeleteByRelatedId(this._id1);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f0.Id));
            this._repo.GetById(this._f1.Id);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f2.Id));
        }
    }

    public class TestEntitySelfRelationRepo : IRAMEntitySelfRelationRepo
    <Foe, Id, int, RelationDTO<Id, int, Id, int>>
    {
        protected override int GetRuleCount()
        {
            var a = new Foe(new Id(1), new Id(2));
            return a.GetUniqueRules().Count();
        }
    }
}