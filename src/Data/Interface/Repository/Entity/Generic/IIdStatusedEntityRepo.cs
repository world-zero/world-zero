using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdStatusedEntity`.
    /// </summary>
    public interface IIdStatusedEntityRepo<TIdStatusedEntity>
        : IIdEntityRepo<TIdStatusedEntity>
        where TIdStatusedEntity : IIdStatusedEntity
    {
        // NOTE: I am leaving the GetByStatusId off until I work on the reading
        // service classes.

        /// <summary>
        /// `Delete()` the entities with the supplied flag.
        /// </summary>
        void DeleteByStatusId(Name statusId);
        Task DeleteByStatusIdAsync(Name statusId);
    }
}