using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IIdNamedEntityUpdate{TEntity}"/>
    public abstract class ABCIdNamedEntityUpdate<TEntity>
        : ABCEntityService<TEntity, Id, int>,
        IIdNamedEntityUpdate<TEntity>
        where TEntity : class, IIdNamedEntity
    {
        public ABCIdNamedEntityUpdate(IIdNamedEntityRepo<TEntity> repo)
            : base(repo)
        { }

        public abstract void AmendName(TEntity e, Name newName);
        public abstract void AmendName(Id entityId, Name newName);
        public abstract Task AmendNameAsync(TEntity e, Name newName);
        public abstract Task AmendNameAsync(Id entityId, Name newName);
    }
}