using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestVoteModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var v = new VoteModel();
            Assert.IsNull(v.Praxis);
            Assert.IsNull(v.Character);
        }
    }
}