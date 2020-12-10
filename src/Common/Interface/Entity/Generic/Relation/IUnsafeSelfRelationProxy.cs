using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeSelfRelationProxy
        <TEntity, TId, TBuiltIn>
        : IUnsafeRelationProxy<TEntity, TId, TBuiltIn, TId, TBuiltIn>
        where TEntity : class, IUnsafeEntity, IEntitySelfRelation
            <TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public IUnsafeSelfRelationProxy(TEntity e)
            : base(e)
        { }
    }
}