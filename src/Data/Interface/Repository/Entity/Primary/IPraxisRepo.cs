using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface IPraxisRepo
        : IIdStatusedEntityRepo<IPraxis>
    {
        /// <summary>
        /// Get a collection of saved Praxises that have a matching
        /// MetaTask.Id as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<IPraxis> GetByMetaTaskId(Id metaTaskId);

        /// <summary>
        /// Get a collection of saved Praxises that are praxises of the task of
        /// the supplied ID. If there are none, an exception is thrown.
        /// </summary>
        IEnumerable<IPraxis> GetByTaskId(Id taskId);
    }
}