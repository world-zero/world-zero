using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Primary;

namespace WorldZero.Service.Constant.Entity.Primary
{
    /// <inheritdoc cref="IConstantEntities"/>
    public class ConstantAbilities : IConstantEntities
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

        public ConstantAbilities(IAbilityRepo repo)
            : base(repo)
        { }

        public override IEnumerable<IAbility> GetEntities()
        {
            yield return Reiterator;
        }
    }
}