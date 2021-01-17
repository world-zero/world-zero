using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestStatusDTO
    {
        private Name _id;
        private string _desc;
        private StatusDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Name("bar");
            this._desc = "foo";
            this._dto = new StatusDTO(this._id, this._desc);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._desc, this._dto.Description);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as StatusDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new StatusDTO(this._id, this._desc);
            Assert.AreEqual(this._dto, alt);
            alt = new StatusDTO(null, this._desc);
            Assert.AreNotEqual(this._dto, alt);
            alt = new StatusDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new StatusDTO(this._id, this._desc);
            Assert.AreEqual(this._dto, alt);
            alt = new StatusDTO(null, this._desc);
            Assert.AreNotEqual(this._dto, alt);
            alt = new StatusDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<StatusDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}