using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IAbilityReg
        : IEntityReg<IAbility, Name, string>
    {
        static readonly UnsafeAbility Reiterator =
            new UnsafeAbility(
                new Name("Reiterator"),
                string.Join("",
                    "This ability allows characters to complete tasks more ",
                    "times than usual."
                )
            );
    }
}