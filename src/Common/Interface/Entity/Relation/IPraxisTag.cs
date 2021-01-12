using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTagDTO"/>
    public interface IPraxisTag : ITaggedEntity<Id, int>
    {
        Id PraxisId { get; }
    }
}
