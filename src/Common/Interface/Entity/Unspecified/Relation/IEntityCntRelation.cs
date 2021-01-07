using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// <summary>
    /// This extends IEntityRelation by adding a counter to the combo-unique
    /// rule of left ID + right ID.
    /// </summary>
    /// </remarks>
    /// Repositories of these entities are responsible for ensuring that there
    /// is a triple uniqueness on left ID, right ID, and the count, whereas
    /// the creation service classes are responsible for ensuring that the
    /// entity's Count is reasonable.
    /// </remarks>
    public interface IEntityCntRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        /// <remarks>
        /// This should always be positive.
        /// </remarks>
        int Count { get; }
    }
}