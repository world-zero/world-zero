using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeIdNameCntRelationProxy<TEntity>
        : IUnsafeCntRelationProxy<TEntity, Id, int, Name, string>
        where TEntity : class, IUnsafeEntity, IIdNameCntRelation
    {
        public IUnsafeIdNameCntRelationProxy(TEntity e)
            : base(e)
        { }
    }
}