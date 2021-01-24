using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IFlaggedDTO{TLeftId, TRightId}"/>
    public interface IFlaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
        IFlaggedDTO<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    { }
}