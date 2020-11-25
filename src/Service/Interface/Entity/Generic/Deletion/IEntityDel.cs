using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <summary>
    /// This service class will handle deleting entities. For more, <see cref=
    /// "IEntityRepo.Delete()"/> and <see cref="IEntityRepo.Save()"/>.
    /// </summary>
    public abstract class IEntityDel<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public IEntityDel(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }

        public virtual void Delete(TId id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            try
            {
                this._repo.Delete(id);
                this.EndTransaction();
            }
            catch (ArgumentException exc)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not delete the supplied entity.", exc);
            }
        }

        public virtual void Delete(TEntity e)
        {
            this.AssertNotNull(e, "e");
            this.Delete(e.Id);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.Delete(id));
        }

        public virtual async Task DeleteAsync(TEntity e)
        {
            this.AssertNotNull(e, "e");
            await Task.Run(() => this.Delete(e.Id));
        }
    }
}