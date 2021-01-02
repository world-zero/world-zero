using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestABCNamedEntity
    {
        private string _sName;
        private TestNamedEntity _nameSupplied;

        [SetUp]
        public void Setup()
        {
            this._sName = "Pizza";
            this._nameSupplied = new TestNamedEntity(this._sName);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(new Name(this._sName), this._nameSupplied.Id);
        }

        [Test]
        public void TestName()
        {
            Assert.Throws<ArgumentException>(()=>this._nameSupplied.Id = new Name("new"));
        }
    }

    public class TestNamedEntity : ABCNamedEntity
    {
        public TestNamedEntity(string name)
            : base(new Name(name))
        {
        }

        public override IEntity<Name, string> CloneAsEntity()
        {
            return null;
        }
    }
}