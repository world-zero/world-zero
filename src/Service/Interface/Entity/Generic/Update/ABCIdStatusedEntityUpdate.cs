using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Service.Constant.Entity.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IIdStatusedEntityUpdate{TEntity}"/>
    public abstract class ABCIdStatusedEntityUpdate<TEntity>
        : ABCEntityUpdate<TEntity, Id, int>,
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

        private void _updateEntity(TEntity e, Name statusId)
        {
            ABCIdStatusedEntity n =
                (ABCIdStatusedEntity) ((IIdStatusedEntity) e);
            n.StatusId = statusId;
        }

        private void _verifyStatus(IStatus s)
        {
            this._verifyStatus(s.Id);
        }

        private void _verifyStatus(Name statusId)
        {
            foreach (IStatus s in ConstantStatuses.StaticGetEntities())
            {
                if (s.Id == statusId)
                    return;
            }
            this._statusRepo.GetById(statusId);
        }

        public virtual void AmendStatus(TEntity e, IStatus newStatus)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newStatus, "newStatus");
            void f()
            {
                this._verifyStatus(newStatus);
                this._updateEntity(e, newStatus.Id);
            }
            this.AmendHelper(f, e);
        }

        public virtual void AmendStatus(Id entityId, IStatus newStatus)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newStatus, "newStatus");
            void f()
            {
                this.AmendStatus(this._repo.GetById(entityId), newStatus);
            }
            this.Transaction(f, true);
        }

        public virtual void AmendStatus(TEntity e, Name newStatusId)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newStatusId, "newStatusId");
            void f()
            {
                this._verifyStatus(newStatusId);
                this._updateEntity(e, newStatusId);
            }
            this.AmendHelper(f, e);
        }

        public virtual void AmendStatus(Id entityId, Name newStatusId)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newStatusId, "newStatusId");
            void f()
            {
                this.AmendStatus(this._repo.GetById(entityId), newStatusId);
            }
            this.Transaction(f, true);
        }

        public async Task AmendStatusAsync(TEntity e, IStatus newStatus)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newStatus, "newStatus");
            await Task.Run(() => this.AmendStatus(e, newStatus));
        }

        public async Task AmendStatusAsync(Id entityId, IStatus newStatus)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newStatus, "newStatus");
            await Task.Run(() => this.AmendStatus(entityId, newStatus));
        }

        public async Task AmendStatusAsync(TEntity e, Name newStatusId)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newStatusId, "newStatusId");
            await Task.Run(() => this.AmendStatus(e, newStatusId));
        }

        public async Task AmendStatusAsync(Id entityId, Name newStatusId)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newStatusId, "newStatusId");
            await Task.Run(() => this.AmendStatus(entityId, newStatusId));
        }
    }
}