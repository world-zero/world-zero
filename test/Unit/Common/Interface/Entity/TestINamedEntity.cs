using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity
{
    [TestFixture]
    public class TestINamedEntity
    {
        private string _sName;
        private TestNamedEntity _nameSupplied;
        private TestNamedEntity _noName;

        [SetUp]
        public void Setup()
        {
            this._sName = "Pizza";
            this._nameSupplied = new TestNamedEntity(this._sName);
            this._noName = new TestNamedEntity();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(new Name(this._sName), this._nameSupplied.Id);
            Assert.IsNull(this._noName.Id);
        }

        [Test]
        public void TestName()
        {
            Assert.Throws<ArgumentException>(()=>this._nameSupplied.Id = new Name("new"));

            this._noName.Id = new Name("Jack");
            Assert.AreEqual(new Name("Jack"), this._noName.Id);
            Assert.Throws<ArgumentException>(()=>this._noName.Id = new Name("new"));
        }
    }

    public class TestNamedEntity : INamedEntity
    {
        public TestNamedEntity()
            : base()
        {
        }

        public TestNamedEntity(string name)
            : base(new Name(name))
        {
        }

        public override IEntity<Name, string> DeepCopy()
        {
            return null;
        }
    }
}