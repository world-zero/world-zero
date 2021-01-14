using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Primary
{
    [TestFixture]
    public class TestIdNamedDTO
    {
        private Id _id;
        private Name _name;
        private IdNamedDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._name = new Name("asdf");
            this._dto = new IdNamedDTO(this._id, this._name);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._name.Get, this._dto.Name.Get);
        }

        [Test]
        public void TestClone()
        {
            IdNamedDTO clone = (IdNamedDTO) this._dto.Clone();
            Assert.AreEqual(this._dto, clone);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new IdNamedDTO(this._id, this._name);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new IdNamedDTO(this._id, this._name);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<IdNamedDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}