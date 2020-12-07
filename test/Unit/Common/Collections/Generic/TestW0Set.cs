using WorldZero.Common.Collections.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Collections.Generic
{
    [TestFixture]
    public class TestW0Set
    {
        [Test]
        public void TestEquals()
        {
            var set0 = new W0Set<int>();
            set0.Add(3);
            set0.Add(7);
            set0.Add(9);
            set0.Add(9);
            set0.Add(324);

            var set1 = new W0Set<int>();
            set1.Add(3);
            set1.Add(3);
            set1.Add(7);
            set1.Add(9);
            set1.Add(324);

            Assert.AreEqual(set0, set1);
            set1.Add(-932);
            Assert.AreNotEqual(set0, set1);

            Assert.IsFalse(set0.Equals(null));
            Assert.IsFalse(set0.Equals("string"));
        }
    }
}