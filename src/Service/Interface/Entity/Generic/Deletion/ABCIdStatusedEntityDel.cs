using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using WorldZero.Common.Interface.Entity.Primary;

// NOTE: `PraxisDel.DeleteByStatus()` is tested in place of
// `IIdStatusedEntityDel.DeleteByStatus()`.

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IIdStatusedEntityDel{TEntity}"/>
    public abstract class ABCIdStatusedEntityDel<TEntity>
        : ABCEntityDel<TEntity, Id, int>,
          IIdStatusedEntityDel<TEntity>
        where TEntity : class, IIdStatusedEntity
    {
        protected IIdStatusedEntityRepo<TEntity> _statusedRepo
        { get { return (IIdStatusedEntityRepo<TEntity>) this._repo; } }

        public ABCIdStatusedEntityDel(IIdStatusedEntityRepo<TEntity> repo)
            : base(repo)
        { }

        public void DeleteByStatus(IStatus s)
        {
            this.AssertNotNull(s, "s");
            this.DeleteByStatus(s.Id);
        }

        public void DeleteByStatus(Name statusId)
        {
            this.AssertNotNull(statusId, "statusId");

            void f(Name status)
            {
                IEnumerable<TEntity> entities;
                try
                {
                    entities = this._statusedRepo.GetByStatusId(status);
                    foreach (TEntity e in entities)
                        this.Delete(e);
                }
                catch (ArgumentException)
                { return; }
            }

            this.Transaction<Name>(f, statusId, true);
        }

        public async Task DeleteByStatusAsync(IStatus s)
        {
            this.AssertNotNull(s, "s");
            await Task.Run(() => this.DeleteByStatus(s));
        }

        public async Task DeleteByStatusAsync(Name statusId)
        {
            this.AssertNotNull(statusId, "statusId");
            await Task.Run(() => this.DeleteByStatus(statusId));
        }
    }
}