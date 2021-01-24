using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <remarks>
    /// Since a relation requires a left and right ID to be set on
    /// initialization, `PraxisId` will default to a `new Id(0)` when
    /// supplied with null. Similarly, if `LeftId` contains `new Id(0)`,
    /// `PraxisId` will return null. This will not apply to `LeftId`, it
    /// will contain `new Id(0)` like normal.
    /// <br />
    /// The above ^^ is not super necessary anymore since DTOs exist, but this
    /// logic is kept just to keep things flexible.
    /// </remarks>
    /// <inheritdoc cref="IPraxisParticipantDTO"/>
    public interface IPraxisParticipant :
        IPraxisParticipantDTO,
        IEntityRelation<Id, int, Id, int>
    { }
}
