using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Relation
{
    [TestFixture]
    public class TestNameNameDTO
    {
        private Name _name0;
        private Name _name1;
        private NameNameDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._name0 = new Name("5");
            this._name1 = new Name("3");
            this._dto = new NameNameDTO(this._name0, this._name1);
        }

        [Test]
        public void TestConstructor()
        {
            new NameNameDTO(this._name0, this._name1);
            Assert.Throws<ArgumentNullException>(
                ()=>new NameNameDTO(null, this._name1));
            Assert.Throws<ArgumentNullException>(
                ()=>new NameNameDTO(this._name0, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new NameNameDTO(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new NameNameDTO(this._name0, this._name1);
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