using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeNameNameCntRelationProxy<TEntity>
        : IUnsafeCntRelationProxy<TEntity, Name, string, Name, string>
        where TEntity : class, IUnsafeEntity, INameNameCntRelation
    {
        public IUnsafeNameNameCntRelationProxy(TEntity e)
            : base(e)
        { }
    }
}