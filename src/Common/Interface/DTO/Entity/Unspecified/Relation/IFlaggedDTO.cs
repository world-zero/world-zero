using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation
{
    /// <remarks>
    /// This will configure the right ID to be a Name, intended to be used as a
    /// relation to a Flag.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationDTO{TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn}"/>
    public interface IFlaggedDTO<TLeftId, TLeftBuiltIn>
        : IEntityRelationDTO<TLeftId, TLeftBuiltIn, TLeftId, TLeftBuiltIn>
        where TLeftId : ABCSingleValueObject<TLeftBuiltIn>
    {
        /// <summary>
        /// This is a wrapper for `RightId`.
        /// </summary>
        Name FlagId { get; }
    }
}