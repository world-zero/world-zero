using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeCntRelationProxy
        <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IUnsafeRelationProxy
            <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TEntity : class, IUnsafeEntity, IEntityCntRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public IUnsafeCntRelationProxy(TEntity e)
            : base(e)
        { }

        public int Count => this._unsafeEntity.Count;
    }
}