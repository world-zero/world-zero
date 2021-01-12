using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IPraxisFlagDTO"/>
    public interface IPraxisFlag : IFlaggedEntity<Id, int>
    {
        Id PraxisId { get; }
    }
}