using System;
using System.Collections.Generic;
using WorldZero.Common.DTO.General.Generic;
using WorldZero.Common.Interface.DTO;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.General.Generic
{
    [TestFixture]
    public class TestCountingDTO
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
            IDTO b = new CountingDTO<string>("fds", 2);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestEqualsAlt()
        {
            var a = new CountingDTO<int>(5, 2);
            var set = new HashSet<CountingDTO<int>>();
            set.Add(a);
            var clone = (CountingDTO<int>) a.Clone();
            Assert.AreEqual(a.GetHashCode(), clone.GetHashCode());
            Assert.IsTrue(set.Contains(a));
            Assert.IsTrue(set.Contains(clone));
        }

        [Test]
        public void TestClone()
        {
            int countee = 234;
            int count = 4;
            var dto = new CountingDTO<int>(countee, count);
            CountingDTO<int> other = (CountingDTO<int>) dto.Clone();
            Assert.AreEqual(countee, other.Countee);
            Assert.AreEqual(count, other.Count);
        }
    }
}