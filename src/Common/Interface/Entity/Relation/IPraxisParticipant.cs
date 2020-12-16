using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IIdIdRelation"/>
    /// <summary>
    /// This relation maps a character's ID to a praxis, allowing for multiple
    /// characters to participate on a single praxis.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `CharacterId`
    /// </summary>
    /// <remarks>
    /// A Praxis should always have at least one participant.
    /// <br />
    /// Since some characters can repeat tasks, but since the praxis is
    /// different for every attempt, this does not track the attempt number.
    /// </remarks>
    public interface IPraxisParticipant : IIdIdRelation
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        /// <remarks>
        /// Since a relation requires a left and right ID to be set on
        /// initialization, `PraxisId` will default to a `new Id(0)` when
        /// supplied with null. Similarly, if `LeftId` contains `new Id(0)`,
        /// `PraxisId` will return null. This will not apply to `LeftId`, it
        /// will contain `new Id(0)` like normal.
        /// </remarks>
        Id PraxisId { get; }

        /// <summary>
        /// CharacterId wraps RightId, which is the ID of the related Character.
        /// </summary>
        Id CharacterId { get; }
    }
}
