using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IIdStatusedEntityUpdate{TEntity}"/>
    public abstract class ABCIdStatusedEntityUpdate<TEntity>
        : ABCEntityService<TEntity, Id, int>,
        IIdStatusedEntityUpdate<TEntity>
        where TEntity : class, IIdStatusedEntity
    {
        protected readonly IStatusRepo _statusRepo;

        public ABCIdStatusedEntityUpdate(
            IIdEntityRepo<TEntity> repo,
            IStatusRepo statusRepo
        )
            : base(repo)
        {
            this.AssertNotNull(statusRepo, "statusRepo");
            this._statusRepo = statusRepo;
        }

        public abstract void AmendStatus(TEntity e, IStatus newStatus);
        public abstract void AmendStatus(TEntity e, Name newStatusId);
        public abstract Task AmendStatusAsync(TEntity e, IStatus newStatus);
        public abstract Task AmendStatusAsync(TEntity e, Name newStatusId);
    }
}