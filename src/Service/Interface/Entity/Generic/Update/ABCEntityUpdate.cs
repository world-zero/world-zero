using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityUpdate<TEntity, TId, TBuiltIn>
        : ABCEntityService<TEntity, TId, TBuiltIn>,
        IEntityUpdate<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public ABCEntityUpdate(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }
    }
}