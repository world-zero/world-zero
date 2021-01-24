using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTagDTO"/>
    public interface IPraxisTag : IPraxisTagDTO, ITaggedEntity<Id, int>
    { }
}
