using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Primary
{
    [TestFixture]
    public class TestAbility
    {
        [Test]
        public void TestDescription()
        {
            var name = new Name("Star Platinum");
            new UnsafeAbility(name, "ORA");
            Assert.Throws<ArgumentException>(()=>new UnsafeAbility(name, "     "));
        }
    }
}