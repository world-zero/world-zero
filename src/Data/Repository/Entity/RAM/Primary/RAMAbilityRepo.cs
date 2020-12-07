using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
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