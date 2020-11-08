using WorldZero.Common.Collections;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Collections
{
    [TestFixture]
    public class TestW0List
    {
        [Test]
        public void TestEquals()
        {
            var list0 = new W0List<int>();
            list0.Add(3);
            list0.Add(7);
            list0.Add(9);
            list0.Add(324);

            var list1 = new W0List<int>();
            list1.Add(3);
            list1.Add(7);
            list1.Add(9);
            list1.Add(324);

            Assert.AreEqual(list0, list1);
            list1.Add(-932);
            Assert.AreNotEqual(list0, list1);

            Assert.IsFalse(list0.Equals(null));
            Assert.IsFalse(list0.Equals("string"));
        }
    }
}