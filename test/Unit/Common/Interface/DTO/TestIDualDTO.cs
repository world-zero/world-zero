using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Inferface.DTO
{
    [TestFixture]
    public class TestIDualDTO
    {
        private Id _id0;
        private Id _id1;
        private TestDualDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._dto = new TestDualDTO(this._id0, this._id1);
        }

        [Test]
        public void TestConstructor()
        {
            new TestDualDTO(this._id0, this._id1);
            Assert.Throws<ArgumentNullException>(
                ()=>new TestDualDTO(null, this._id1));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestDualDTO(this._id1, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestDualDTO(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new TestDualDTO(this._id0, this._id1);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }
    }

    public class TestDualDTO : IDualDTO<Id, int, Id, int>
    {
        public TestDualDTO(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        // This is not relevant so it is not built.
        public override IDualDTO<Id, int, Id, int> DeepCopy()
        {
            return null;
        }
    }
}