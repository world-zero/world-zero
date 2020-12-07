using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdNamedEntityRepo"/>
    public interface IUnsafeCharacterRepo
        : IIdNamedEntityRepo<UnsafeCharacter>
    {
        /// <summary>
        /// Get a collection of saved Characters that have the supplied
        /// PlayerId. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<UnsafeCharacter> GetByPlayerId(Id playerId);

        /// <summary>
        /// Get a collection of saved Characters that have a matching
        /// LocationId as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<UnsafeCharacter> GetByLocationId(Id locationId);

        /// <summary>
        /// Get a collection of saved Characters that have a matching
        /// Faction name as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<UnsafeCharacter> GetByFactionId(Name factionId);

        /// <remarks>
        /// The level is determined by processing the player's characters and
        /// finding the character(s) with the highest era or total level, and
        /// using the the larger of which for the returned level. This will
        /// throw an ArgumentException if the supplied playerId is not
        /// associated with any characters.
        /// </remarks>
        Level FindHighestLevel(UnsafePlayer player);
        Level FindHighestLevel(Id playerId);
        Task<Level> FindHighestLevelAsync(UnsafePlayer player);
        Task<Level> FindHighestLevelAsync(Id playerId);
    }
}