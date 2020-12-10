using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeNameNameRelationProxy<TEntity>
        : IUnsafeRelationProxy<TEntity, Name, string, Name, string>
        where TEntity : class, IUnsafeEntity, INameNameRelation
    {
        public IUnsafeNameNameRelationProxy(TEntity e)
            : base(e)
        { }
    }
}