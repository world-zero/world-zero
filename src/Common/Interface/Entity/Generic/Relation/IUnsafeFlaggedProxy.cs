using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeFlaggedProxy
        <TEntity, TLeftId, TLeftBuiltIn>
        : IUnsafeRelationProxy
            <TEntity, TLeftId, TLeftBuiltIn, Name, string>
        where TEntity : class, IUnsafeEntity, IFlaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
    {
        public IUnsafeFlaggedProxy(TEntity e)
            : base(e)
        { }
    }
}