using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IFactionRepo"/>
    public class RAMFactionRepo
        : IRAMNamedEntityRepo<IFaction>,
        IFactionRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeFaction(new Name("x"), new PastDate(DateTime.UtcNow));
            return a.GetUniqueRules().Count;
        }

        public IEnumerable<IFaction> GetByAbilityId(Name abilityId)
        {
            if (abilityId == null)
                throw new ArgumentNullException("abilityId");

            IEnumerable<IFaction> factions =
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