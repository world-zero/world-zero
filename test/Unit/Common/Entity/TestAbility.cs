using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestAbility
    {
        [Test]
        public void TestDescription()
        {
            var name = new Name("Star Platinum");
            new Ability(name, "ORA");
            Assert.Throws<ArgumentException>(()=>new Ability(name, "     "));
        }
    }
}