using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation
{
    /// <remarks>
    /// This will configure the right ID to be a Name, intended to be used as a
    /// relation to a Tag.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationDTO{TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn}"/>
    public interface ITaggedDTO<TId, TBuiltIn>
        : IEntityRelationDTO<TId, TBuiltIn, TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        /// <summary>
        /// This is a wrapper for `RightId`.
        /// </summary>
        Name TagId { get; }
    }
}