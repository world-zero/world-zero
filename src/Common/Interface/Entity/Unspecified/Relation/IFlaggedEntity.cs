using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IFlaggedDTO{TLeftId, TRightId}"/>
    public interface IFlaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    {
        Name FlagId { get; }
    }
}