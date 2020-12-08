using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdStatusedEntity`.
    /// </summary>
    public interface IIdStatusedEntityRepo<TEntity>
        : IIdEntityRepo<TEntity>
        where TEntity : UnsafeIIdStatusedEntity
    {
        /// <summary>
        /// Get a collection of entities with the supplied status ID. If none
        /// exists, an exception is thrown.
        /// </summary>
        IEnumerable<TEntity> GetByStatusId(Name statusId);

        // Deleting by status ID is a very bad idea as entities could need to
        // cascade their deletion.
    }
}