using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

// NOTE: `PraxisDel.DeleteByStatus()` is tested in place of
// `IIdStatusedEntityDel.DeleteByStatus()`.

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// This service class will handle deleting entities. For more, <see cref=
    /// "IEntityRepo.Delete()"/>, <see cref="IEntityRepo.Save()"/>, and <see
    /// cref="ABCIdStatusedEntity"/>.
    /// </summary>
    public abstract class IIdStatusedEntityDel<TEntity>
        : IEntityDel<TEntity, Id, int>
        where TEntity : ABCIdStatusedEntity
    {
        protected IIdStatusedEntityRepo<TEntity> _statusedRepo
        { get { return (IIdStatusedEntityRepo<TEntity>) this._repo; } }

        public IIdStatusedEntityDel(IIdStatusedEntityRepo<TEntity> repo)
            : base(repo)
        { }

        public void DeleteByStatus(UnsafeStatus s)
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

        public async
        System.Threading.Tasks.Task DeleteByStatusAsync(UnsafeStatus s)
        {
            this.AssertNotNull(s, "s");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByStatus(s));
        }

        public async
        System.Threading.Tasks.Task DeleteByStatusAsync(Name statusId)
        {
            this.AssertNotNull(statusId, "statusId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByStatus(statusId));
        }
    }
}