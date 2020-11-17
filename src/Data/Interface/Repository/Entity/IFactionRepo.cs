using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface IFactionRepo
        : INamedEntityRepo<Faction>
    {
        /// <summary>
        /// Get a collection of saved Factions that have a matching
        /// Ability name as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Faction> GetByAbilityId(Name abilityId);
    }
}