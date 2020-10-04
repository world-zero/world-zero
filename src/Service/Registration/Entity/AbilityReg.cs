using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class AbilityReg
        : IEntityReg<Ability, Name, string>
    {
        protected IAbilityRepo _abilityRepo
        { get { return (IAbilityRepo) this._repo; } }

        public AbilityReg(IAbilityRepo abilityRepo)
            : base(abilityRepo)
        { }
    }
}