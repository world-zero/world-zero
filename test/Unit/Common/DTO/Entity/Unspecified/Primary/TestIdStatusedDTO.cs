using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Primary
{
    [TestFixture]
    public class TestIdStatusedDTO
    {
        private Id _id;
        private Name _statusId;
        private IdStatusedDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._statusId = new Name("asdf");
            this._dto = new IdStatusedDTO(this._id, this._statusId);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._statusId.Get, this._dto.StatusId.Get);
        }

        [Test]
        public void TestClone()
        {
            IdStatusedDTO clone = (IdStatusedDTO) this._dto.Clone();
            Assert.AreEqual(this._dto, clone);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new IdStatusedDTO(this._id, this._statusId);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new IdStatusedDTO(this._id, this._statusId);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<IdStatusedDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}