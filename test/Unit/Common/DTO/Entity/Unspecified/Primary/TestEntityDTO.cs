using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Primary
{
    [TestFixture]
    public class TestEntityDTO
    {
        private Id _id;
        private EntityDTO<Id, int> _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._dto = new EntityDTO<Id, int>(this._id);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as EntityDTO<Id, int>);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new EntityDTO<Id, int>(this._id);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new EntityDTO<Id, int>(this._id);
            Assert.AreEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<EntityDTO<Id, int>>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}