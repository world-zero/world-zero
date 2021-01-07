using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// <remarks>
    /// This will configure the right ID to be a Name, intended to be used as a
    /// relation to a Flag.
    /// </remarks>
    public interface IFlaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    {
        /// <summary>
        /// This is a wrapper for `RightId`.
        /// </summary>
        Name FlagId { get; }
    }
}