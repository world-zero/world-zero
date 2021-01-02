using System;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject.DTO.General.Generic
{
    [TestFixture]
    public class TestFrequencyDTO
    {
        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new CountingDTO<string>(null, 2));
            Assert.Throws<ArgumentException>(()=>
                new CountingDTO<string>("sdf", -1));
            new CountingDTO<string>("sdf", 0);
        }

        [Test]
        public void TestEquals()
        {
            var a = new CountingDTO<string>("fds", 2);
            var b = new CountingDTO<string>("fds", 2);
            Assert.AreEqual(a, b);
        }
    }
}