using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeIdIdCntRelationProxy<TEntity>
        : IUnsafeCntRelationProxy<TEntity, Id, int, Id, int>
        where TEntity : class, IUnsafeEntity, IIdIdCntRelation
    {
        public IUnsafeIdIdCntRelationProxy(TEntity e)
            : base(e)
        { }
    }
}