using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeIdIdRelationProxy<TEntity>
        : IUnsafeRelationProxy<TEntity, Id, int, Id, int>
        where TEntity : class, IUnsafeEntity, IIdIdRelation
    {
        public IUnsafeIdIdRelationProxy(TEntity e)
            : base(e)
        { }
    }
}