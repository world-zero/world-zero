using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
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