using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IIdNamedEntityRepo"/>
    public interface ICharacterRepo
        : IIdNamedEntityRepo<Character>
    {
        /// <summary>
        /// Get a collection of saved Character that have the supplied
        /// PlayerId. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Character> GetByPlayerId(Id playerId);
    }
}