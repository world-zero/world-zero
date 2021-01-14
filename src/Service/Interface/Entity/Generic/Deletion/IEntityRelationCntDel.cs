using System.Threading.Tasks;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

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
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
        where TRelationDTO : NoIdCntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        void DeleteByPartialDTO(
            NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );

        Task DeleteByPartialDTOAsync(
            NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );
    }
}