using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelationRepo"/>
    /// <remarks>
    /// This will not change DeleteByLeftId() and DeleteByRightId, but instead
    /// add DeleteByRelatedId(), which will check both the left and right
    /// fields for the supplied ID. This applies to other operations as relevant.
    /// </remarks>
    public interface IEntitySelfRelationRepo
    <
        TEntityRelation,
        TId,
        TBuiltIn,
        TRelationDTO
    >
        : IEntityRelationRepo
            <TEntityRelation, TId, TBuiltIn, TId, TBuiltIn, TRelationDTO>
        where TId : ISingleValueObject<TBuiltIn>
        where TEntityRelation : IEntitySelfRelation
            <TId, TBuiltIn>
        where TRelationDTO : RelationDTO
            <TId, TBuiltIn, TId, TBuiltIn>
    {
        /// <summary>
        /// `Delete()` the relations that have the supplied ID in either the
        /// left or right slot.
        /// </summary>
        void DeleteByRelatedId(TId id);
        Task DeleteByRelatedIdAsync(TId id);
    }
}