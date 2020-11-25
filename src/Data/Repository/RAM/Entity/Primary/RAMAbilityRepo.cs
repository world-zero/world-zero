using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity.Primary
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