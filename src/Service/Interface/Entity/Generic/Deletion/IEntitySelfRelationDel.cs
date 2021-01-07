using System.Threading.Tasks;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    /// <summary>
    /// This abstract subclass is used for relations between the same entity.
    /// This results in `DeleteByLeftId()` and `DeleteByRightId()` performing
    /// the same process to ensure that the order of the relation is ignored.
    ///</summary>
    public interface IEntitySelfRelationDel
    <
        TEntityRelation,
        TEntity,
        TId,
        TBuiltIn,
        TRelationDTO
    >
        : IEntityRelationDel
        <
            TEntityRelation,
            TEntity,
            TId,
            TBuiltIn,
            TEntity,
            TId,
            TBuiltIn,
            TRelationDTO
        >
        where TEntityRelation : class, IEntitySelfRelation
            <TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId  : ABCSingleValueObject<TBuiltIn>
        where TRelationDTO : RelationDTO
            <TId, TBuiltIn, TId, TBuiltIn>
    {
        void DeleteByRelatedId(TId id);
        Task DeleteByRelatedIdAsync(TId id);
    }
}