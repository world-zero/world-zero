using System;
using System.Linq;
using System.Collections.Generic;
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

        public IEnumerable<Faction> GetByAbilityId(Name abilityId)
        {
            if (abilityId == null)
                throw new ArgumentNullException("abilityId");

            IEnumerable<Faction> factions =
                from p in this._saved.Values
                let praxis = this.TEntityCast(p)
                where praxis.AbilityId != null
                where praxis.AbilityId == abilityId
                select praxis;

            if (factions.Count() == 0)
                throw new ArgumentException($"There are no praxises with a meta task ID of {abilityId}");
            else
                return factions;
        }
    }
}