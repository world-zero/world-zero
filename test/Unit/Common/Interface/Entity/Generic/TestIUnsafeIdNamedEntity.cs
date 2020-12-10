using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestIUnsafeIdNamedEntity
    {
        private string _name;
        private TestIdNamedEntity _e;

        [SetUp]
        public void Setup()
        {
            this._name = "Pizza";
            this._e = new TestIdNamedEntity(this._name);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(new Name(this._name), this._e.Name);
            Assert.Throws<ArgumentException>(()=>new TestIdNamedEntity(null));
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual(new Name(this._name), this._e.Name);
            this._e.Name = new Name("Steve");
            Assert.AreEqual(new Name("Steve"), this._e.Name);
            Assert.Throws<ArgumentNullException>(()=>this._e.Name = null);
            Assert.AreEqual(new Name("Steve"), this._e.Name);
        }

        [Test]
        public void TestGetUniqueRules()
        {
            var combos = this._e.GetUniqueRules();
            Assert.IsNotNull(combos);
            bool hasNameCombo = false;
            foreach (IEnumerable<object> iterable in combos)
            {
                foreach (object o in iterable)
                {
                    var name = o as Name;
                    if ( (name != null) && (new Name(this._name) == name) )
                        hasNameCombo = true;
                }
            }
            Assert.IsTrue(hasNameCombo);
        }
    }

    public class TestIdNamedEntity : IUnsafeIdNamedEntity
    {
        public TestIdNamedEntity(string name)
            : base(new Name(name))
        { }

        public override IEntity<Id, int> Clone()
        {
            return null;
        }
    }
}