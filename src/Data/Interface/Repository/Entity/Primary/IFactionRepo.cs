using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface IFactionRepo
        : INamedEntityRepo<UnsafeFaction>
    {
        /// <summary>
        /// Get a collection of saved Factions that have a matching
        /// Ability name as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<UnsafeFaction> GetByAbilityId(Name abilityId);
    }
}