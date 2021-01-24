using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation
{
    /// <summary>
    /// This DTO represents a many-to-many mapping between two Entity DTOs by
    /// containing their IDs.
    /// </summary>
    /// <inheritdoc cref="IEntityDTO{TId, TBuiltIn}"/>
    public interface IEntityRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IEntityDTO<Id, int>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        TLeftId LeftId { get; }
        TRightId RightId { get; }
        NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> GetNoIdRelationDTO();
    }
}