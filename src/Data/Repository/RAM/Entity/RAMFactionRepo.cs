using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
{
    /// <inheritdoc cref="IFactionRepo"/>
    public class RAMFactionRepo
        : IRAMNamedEntityRepo<Faction>,
        IFactionRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Faction(new Name("x"), new PastDate(DateTime.UtcNow));
            return a.GetUniqueRules().Count;
        }
    }
}