using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Relation
{
    [TestFixture]
    public class TestIdNameDTO
    {
        private Id _id0;
        private Name _name0;
        private IdNameDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._name0 = new Name("3");
            this._dto = new IdNameDTO(this._id0, this._name0);
        }

        [Test]
        public void TestConstructor()
        {
            new IdNameDTO(this._id0, this._name0);
            Assert.Throws<ArgumentNullException>(
                ()=>new IdNameDTO(null, this._name0));
            Assert.Throws<ArgumentNullException>(
                ()=>new IdNameDTO(this._id0, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new IdNameDTO(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new IdNameDTO(this._id0, this._name0);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }

        [Test]
        public void TestClone()
        {
            // Yes this assumes that this method doesn't return `this`, but
            // that seems like a safe assumption.
            Assert.AreEqual(this._dto, this._dto.Clone());
        }
    }
}