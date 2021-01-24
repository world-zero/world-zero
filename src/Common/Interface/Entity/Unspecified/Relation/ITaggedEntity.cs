using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="ITaggedDTO{TLeftId, TRightId}"/>
    public interface ITaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
        ITaggedDTO<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    { }
}