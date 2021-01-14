using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation
{
    /// <summary>
    /// This extends IEntityRelationDTO by adding a counter to the combo-unique
    /// rule of left ID + right ID.
    /// </summary>
    /// </remarks>
    /// Repositories of these entities are responsible for ensuring that there
    /// is a triple uniqueness on left ID, right ID, and the count, whereas
    /// the creation service classes are responsible for ensuring that the
    /// entity's Count is reasonable.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationDTO{TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn}"/>
    public interface IEntityCntRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IEntityRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        /// <remarks>
        /// This should always be positive, but whether or not this is enforced
        /// is up to the implementation.
        /// </remarks>
        int Count { get; }
    }
}