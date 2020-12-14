using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IAbilityReg"/>
    public class AbilityReg
        : ABCEntityReg<IAbility, Name, string>, IAbilityReg
    {
        protected IAbilityRepo _abilityRepo
        { get { return (IAbilityRepo) this._repo; } }

        public AbilityReg(IAbilityRepo abilityRepo)
            : base(abilityRepo)
        {
            this.EnsureExists(IAbilityReg.Reiterator);
        }
    }
}