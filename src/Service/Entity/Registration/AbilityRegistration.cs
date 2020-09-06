using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
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