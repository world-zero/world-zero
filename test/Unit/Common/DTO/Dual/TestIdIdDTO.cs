using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using NUnit.Framework;

namespace WorldZero.Test.Common.DTO.Dual
{
    [TestFixture]
    public class TestIdIdDTO
    {
        private Id _id0;
        private Id _id1;
        private IdIdDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._dto = new IdIdDTO(this._id0, this._id1);
        }

        [Test]
        public void TestConstructor()
        {
            new IdIdDTO(this._id0, this._id1);
            Assert.Throws<ArgumentNullException>(
                ()=>new IdIdDTO(null, this._id1));
            Assert.Throws<ArgumentNullException>(
                ()=>new IdIdDTO(this._id1, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new IdIdDTO(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new IdIdDTO(this._id0, this._id1);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }

        [Test]
        public void TestDeepCopy()
        {
            // Yes this assumes that this method doesn't return `this`, but
            // that seems like a safe assumption.
            Assert.AreEqual(this._dto, this._dto.DeepCopy());
        }
    }
}