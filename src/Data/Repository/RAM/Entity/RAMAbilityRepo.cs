using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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