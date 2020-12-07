using System;
using System.Linq;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

// NOTE: This does not test `Unset()` as that is done by Location.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestMetaTaskDel
    {
        private int _nxt = 10000;
        private Id _next() => new Id(this._nxt++);

        private void _absentt<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> getById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private RAMUnsafeMetaTaskRepo _repo;
        private RAMUnsafePraxisRepo _praxisRepo;
        private MetaTaskUnset _unset;
        private UnsafeFaction _faction0;
        private UnsafeFaction _faction1;
        private UnsafeMetaTask _mt0_0;
        private UnsafeMetaTask _mt0_1;
        private UnsafeMetaTask _mt1_0;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMUnsafeMetaTaskRepo();
            this._praxisRepo = new RAMUnsafePraxisRepo();
            this._unset = new MetaTaskUnset(
                this._repo,
                this._praxisRepo
            );

            var s = new Name("status");
            var pt = new PointTotal(2);
            this._faction0 = new UnsafeFaction(new Name("0"));
            this._faction1 = new UnsafeFaction(new Name("1"));
            this._mt0_0 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt0_1 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt1_0 = new UnsafeMetaTask(this._faction1.Id, s, "x", pt);
            this._repo.Insert(this._mt0_0);
            this._repo.Insert(this._mt0_1);
            this._repo.Insert(this._mt1_0);
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
        public void TestDeleteByFactionSad()
        {
            Name name = null;
            UnsafeFaction faction = null;
            Assert.Throws<ArgumentNullException>(()=>this._unset.DeleteByFaction(name));
            Assert.Throws<ArgumentNullException>(()=>this._unset.DeleteByFaction(faction));

            this._unset.DeleteByFaction(new Name("faaaaakeeee 13"));
        }

        [Test]
        public void TestDeleteByFaction_faction0()
        {
            this._unset.DeleteByFaction(this._faction0);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt0_0, this._repo.GetById);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt0_1, this._repo.GetById);
            this._present<UnsafeMetaTask, Id, int>(this._mt1_0, this._repo.GetById);
        }

        [Test]
        public void TestDeleteByFaction_faction1()
        {
            this._unset.DeleteByFaction(this._faction1);
            this._present<UnsafeMetaTask, Id, int>(this._mt0_0, this._repo.GetById);
            this._present<UnsafeMetaTask, Id, int>(this._mt0_1, this._repo.GetById);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt1_0, this._repo.GetById);
        }

        [Test]
        public void TestConstructor()
        {
            new MetaTaskUnset(
                this._repo,
                this._praxisRepo
            );
            Assert.Throws<ArgumentNullException>(()=>new MetaTaskUnset(
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new MetaTaskUnset(
                null,
                this._praxisRepo
            ));
            Assert.Throws<ArgumentNullException>(()=>new MetaTaskUnset(
                this._repo,
                null
            ));
        }
    }
}