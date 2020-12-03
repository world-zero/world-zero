using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface IPraxisRepo
        : IIdStatusedEntityRepo<Praxis>
    {
        /// <summary>
        /// Get a collection of saved Praxises that have a matching
        /// MetaTask.Id as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Praxis> GetByMetaTaskId(Id metaTaskId);

        /// <summary>
        /// Get a collection of saved Praxises that are praxises of the task of
        /// the supplied ID. If there are none, an exception is thrown.
        /// </summary>
        IEnumerable<Praxis> GetByTaskId(Id taskId);
    }
}