using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestPlayerDTO
    {
        private Id _id;
        private Name _name;
        private PlayerDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._name = new Name("foo");
            this._dto = new PlayerDTO(this._id, this._name);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._name, this._dto.Name);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as PlayerDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new PlayerDTO(this._id, this._name);
            Assert.AreEqual(this._dto, alt);
            alt = new PlayerDTO(null, this._name);
            Assert.AreNotEqual(this._dto, alt);
            alt = new PlayerDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new PlayerDTO(this._id, this._name);
            Assert.AreEqual(this._dto, alt);
            alt = new PlayerDTO(null, this._name);
            Assert.AreNotEqual(this._dto, alt);
            alt = new PlayerDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<PlayerDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}