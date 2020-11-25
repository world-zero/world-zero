using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdNamedEntityRepo"/>
    public interface ICharacterRepo
        : IIdNamedEntityRepo<Character>
    {
        /// <summary>
        /// Get a collection of saved Characters that have the supplied
        /// PlayerId. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Character> GetByPlayerId(Id playerId);

        /// <summary>
        /// Get a collection of saved Characters that have a matching
        /// LocationId as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Character> GetByLocationId(Id locationId);

        /// <summary>
        /// Get a collection of saved Characters that have a matching
        /// Faction name as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Character> GetByFactionId(Name factionId);

        /// <remarks>
        /// The level is determined by processing the player's characters and
        /// finding the character(s) with the highest era or total level, and
        /// using the the larger of which for the returned level. This will
        /// throw an ArgumentException if the supplied playerId is not
        /// associated with any characters.
        /// </remarks>
        Level FindHighestLevel(Player player);
        /// <remarks>
        /// The level is determined by processing the player's characters and
        /// finding the character(s) with the highest era or total level, and
        /// using the the larger of which for the returned level. This will
        /// throw an ArgumentException if the supplied playerId is not
        /// associated with any characters.
        /// </remarks>
        Level FindHighestLevel(Id playerId);
    }
}