using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// <remarks>
    /// This will configure the right ID to be a Name, intended to be used as a
    /// relation to a Tag.
    /// </remarks>
    public interface ITaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
    {
        /// <summary>
        /// This is a wrapper for `RightId`.
        /// </summary>
        Name TagId { get; }
    }
}