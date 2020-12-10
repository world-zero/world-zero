using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeIdNameRelationProxy<TEntity>
        : IUnsafeRelationProxy<TEntity, Id, int, Name, string>
        where TEntity : class, IUnsafeEntity, IIdNameRelation
    {
        public IUnsafeIdNameRelationProxy(TEntity e)
            : base(e)
        { }
    }
}