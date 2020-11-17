using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using System.Collections.Generic;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface IPraxisRepo
        : IIdEntityRepo<Praxis>
    {
        /// <summary>
        /// This will return the number of praxises a character has with
        /// statuses that exist in `statuses`.
        /// </summary>
        /// <returns>
        /// This will return 0 if there are no statuses in the set.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This is thrown if the related table for `PraxisParticipant`s cannot
        /// be found.
        /// </exception>
        int GetPraxisCount(Id characterId, ISet<Name> statuses);

        /// <summary>
        /// Get a collection of saved Praxises that have a matching
        /// MetaTask.Id as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Praxis> GetByMetaTaskId(Id metaTaskId);
    }
}