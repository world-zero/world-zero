using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface IFactionRepo
        : INamedEntityRepo<IFaction>
    {
        /// <summary>
        /// Get a collection of saved Factions that have a matching
        /// Ability name as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<IFaction> GetByAbilityId(Name abilityId);
    }
}