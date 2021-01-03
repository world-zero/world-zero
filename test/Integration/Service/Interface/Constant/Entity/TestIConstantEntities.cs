using System;
using NUnit.Framework;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Service.Interface.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Primary;

namespace WorldZero.Test.Integration.Service.Interface.Constant.Entity
{
    [TestFixture]
    public class TestIConstantEntities
    {
        private IAbilityRepo _repo;
        private TestConstantEntities _const;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMAbilityRepo();
            this._const = new TestConstantEntities(this._repo);
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestEnsureExists()
        {
            this._repo.CleanAll();
            var a = new UnsafeAbility(new Name("sdf"), "fds");
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(a.Id));
            this._const.EnsureExists(a);
            Assert.IsNotNull(this._repo.GetById(a.Id));
            this._const.EnsureExists(a);
            Assert.IsNotNull(this._repo.GetById(a.Id));
        }

        [Test]
        public void TestEnsureEntitiesExists()
        {
            this._repo.CleanAll();
            this._const.EnsureEntitiesExist();
            int cnt = 0;
            foreach (IAbility a in this._const.GetEntities())
            {
                cnt++;
                this._repo.GetById(a.Id);
            }
            Assert.AreEqual(2, cnt);
        }
    }

    public class TestConstantEntities : IConstantEntities
        <IAbilityRepo, IAbility, Name, string>,
        IAbilityService
    {
        public static readonly IAbility Reiterator =
            new UnsafeAbility(
                new Name("Reiterator"),
                string.Join("",
                    "This ability allows characters to complete tasks more ",
                    "times than usual."
                )
            );

        public new void EnsureExists(IAbility a)
            => base.EnsureExists(a);

        public static readonly IAbility Foo =
            new UnsafeAbility(new Name("foo"), "idk");

        public TestConstantEntities(IAbilityRepo repo)
            : base(repo)
        { }

        public override IEnumerable<IAbility> GetEntities()
        {
            yield return Reiterator;
            yield return Foo;
        }
    }
}