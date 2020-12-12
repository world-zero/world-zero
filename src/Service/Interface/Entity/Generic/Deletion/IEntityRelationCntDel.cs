using System.Threading.Tasks;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IEntityRelationCntDel
    <
        TEntityRelationCnt,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IEntityRelationDel
        <
            TEntityRelationCnt,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            TRightEntity,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        where TEntityRelationCnt : class, IEntityCntRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : CntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        void DeleteByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );

        Task DeleteByPartialDTOAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );
    }
}