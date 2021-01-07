using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityDel<TEntity, TId, TBuiltIn>
        : ABCEntityService<TEntity, TId, TBuiltIn>,
          IEntityDel<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        public ABCEntityDel(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }

        public virtual void Delete(TId id)
        {
            this.Transaction<TId>(this._repo.Delete, id);
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