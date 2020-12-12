using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class AbilityReg
        : ABCEntityReg<UnsafeAbility, Name, string>
    {
        public static readonly UnsafeAbility Reiterator =
            new UnsafeAbility(
                new Name("Reiterator"),
                string.Join("",
                    "This ability allows characters to complete tasks more ",
                    "times than usual."
                )
            );

        protected IAbilityRepo _abilityRepo
        { get { return (IAbilityRepo) this._repo; } }

        public AbilityReg(IAbilityRepo abilityRepo)
            : base(abilityRepo)
        {
            this.EnsureExists(Reiterator);
        }
    }
}