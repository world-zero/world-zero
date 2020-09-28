using WorldZero.Service.Interface.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class AbilityRegistration
        : IEntityRegistration<Ability, Name, string>
    {
        protected IAbilityRepo _abilityRepo
        { get { return (IAbilityRepo) this._repo; } }

        public AbilityRegistration(IAbilityRepo abilityRepo)
            : base(abilityRepo)
        { }
    }
}