using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TestPraxisParticipant
    {
        [Test]
        public void TestSubmissionCount()
        {
            var pp = new PraxisParticipant(new Id(5), new Id(2));
            Assert.AreEqual(1, pp.SubmissionCount);
            pp = new PraxisParticipant(new Id(5), new Id(2), 7);
            Assert.AreEqual(7, pp.SubmissionCount);
            pp.SubmissionCount = 1;
            Assert.Throws<ArgumentException>(()=>pp.SubmissionCount = 0);
        }
    }
}