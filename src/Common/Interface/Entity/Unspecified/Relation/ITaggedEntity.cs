using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="ITaggedDTO{TLeftId, TRightId}"/>
    public interface ITaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    {
        Name TagId { get; }
    }
}