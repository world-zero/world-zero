using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IIdStatusedEntityUpdate<TEntity>
        : IEntityUpdate<TEntity, Id, int>
        where TEntity : class, IEntity<Id, int>
    {
        void AmendStatus(TEntity e, IStatus newStatus);
        void AmendStatus(Id entityId, IStatus newStatus);
        void AmendStatus(TEntity e, Name newStatusId);
        void AmendStatus(Id entityId, Name newStatusId);
        Task AmendStatusAsync(TEntity e, IStatus newStatus);
        Task AmendStatusAsync(Id entityId, IStatus newStatus);
        Task AmendStatusAsync(TEntity e, Name newStatusId);
        Task AmendStatusAsync(Id entityId, Name newStatusId);
    }
}