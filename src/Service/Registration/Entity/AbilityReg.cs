using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class AbilityReg
        : IEntityReg<Ability, Name, string>
    {
        public static readonly Ability Reiterator =
            new Ability(
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