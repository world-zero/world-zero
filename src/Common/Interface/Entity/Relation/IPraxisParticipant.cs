using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipantDTO"/>
    public interface IPraxisParticipant : IEntityRelation<Id, int, Id, int>
    {
        /// <remarks>
        /// Since a relation requires a left and right ID to be set on
        /// initialization, `PraxisId` will default to a `new Id(0)` when
        /// supplied with null. Similarly, if `LeftId` contains `new Id(0)`,
        /// `PraxisId` will return null. This will not apply to `LeftId`, it
        /// will contain `new Id(0)` like normal.
        /// </remarks>
        Id PraxisId { get; }

        Id CharacterId { get; }
    }
}
