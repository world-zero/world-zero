using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeRelationProxy
        <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IIdUnsafeProxy<TEntity>
        where TEntity : class, IUnsafeEntity, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public IUnsafeRelationProxy(TEntity e)
            : base(e)
        { }

        public TLeftId LeftId => this._unsafeEntity.LeftId;
        public TRightId RightId => this._unsafeEntity.RightId;
        public
        RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> GetDTO()
            => this._unsafeEntity.GetDTO();
    }
}