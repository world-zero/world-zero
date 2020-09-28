using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="IAbilityRepo"/>
    public class RAMAbilityRepo
        : IRAMNamedEntityRepo<Ability>,
        IAbilityRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Ability(new Name("Sawyer"), "dsf");
            return a.GetUniqueRules().Count;
        }
    }
}