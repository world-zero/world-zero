using System;
using NUnit.Framework;
using WorldZero.Data.Repository.RAM.Entity.Primary;
using WorldZero.Service.Entity.Deletion.Primary;

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestEraDel
    {
        [Test]
        public void TestCannotInstantiate()
        {
            Assert.Throws<NotImplementedException>(()=>
                new EraDel(new RAMEraRepo()));
        }
    }
}