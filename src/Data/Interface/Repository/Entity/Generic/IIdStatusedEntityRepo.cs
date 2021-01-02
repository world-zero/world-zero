using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Generic
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdStatusedEntity`.
    /// </summary>
    public interface IIdStatusedEntityRepo<TEntity>
        : IIdEntityRepo<TEntity>
        where TEntity : class, IIdStatusedEntity
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