using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity.Registration
{
    [TestFixture]
    public class TestICharacterReg
    {
        [Test]
        public void TestMinLevelToRegister()
        {
            Assert.IsNotNull(ICharacterReg.MinLevelToRegister);
            Assert.Throws<ArgumentNullException>(()=>
                ICharacterReg.MinLevelToRegister=null);
            var l = new Level(234);
            ICharacterReg.MinLevelToRegister = l;
            Assert.AreEqual(l, ICharacterReg.MinLevelToRegister);
        }
    }
}