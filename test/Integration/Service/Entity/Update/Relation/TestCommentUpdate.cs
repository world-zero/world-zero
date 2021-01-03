using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Update.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Update.Relation
{
    [TestFixture]
    public class TestCommentUpdate
    {
        private string _value;
        private string _newValue;
        private IComment _c;
        private RAMCommentRepo _repo;
        private CommentUpdate _update;

        private void _assertIsOld()
        {
            Assert.AreEqual(
                this._value,
                this._repo.GetById(this._c.Id).Value);
        }

        private void _assertIsNew()
        {
            Assert.AreEqual(
                this._newValue,
                this._repo.GetById(this._c.Id).Value);
        }

        [SetUp]
        public void Setup()
        {
            this._value = "Thi sis a tets";
            this._newValue = "A new desc";
            this._c = new UnsafeComment(new Id(2), new Id(4), this._value);
            this._repo = new RAMCommentRepo();
            this._repo.Insert(this._c);
            this._repo.Save();
            this._update = new CommentUpdate(this._repo);
            this._assertIsOld();
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
        public void TestAmendValueGivenCommentSad()
        {
            IComment c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendValue(c, this._newValue));
            this._assertIsOld();

            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendValue(this._c, null));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendValue(this._c, ""));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendValue(this._c, "   "));
            this._assertIsOld();
        }

        [Test]
        public void TestAmendValueGivenCommentHappy()
        {
            this._assertIsOld();
            this._update.AmendValue(this._c, this._newValue);
            this._assertIsNew();

            this._update.AmendValue(this._c, this._value);
            this._assertIsOld();
        }

        [Test]
        public void TestAmendValueGivenIdSad()
        {
            Id id = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendValue(id, this._newValue));
            this._assertIsOld();

            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendValue(this._c.Id, null));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendValue(this._c.Id, ""));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendValue(this._c.Id, "   "));
            this._assertIsOld();
        }

        [Test]
        public void TestAmendValueGivenIdHappy()
        {
            this._assertIsOld();
            this._update.AmendValue(this._c.Id, this._newValue);
            this._assertIsNew();

            this._update.AmendValue(this._c.Id, this._value);
            this._assertIsOld();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>new CommentUpdate(null));
        }
    }
}