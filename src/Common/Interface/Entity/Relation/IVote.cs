using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IVoteDTO"/>
    public interface IVote : IEntityRelation<Id, int, Id, int>
    {
        Id CharacterId { get; }
        Id PraxisParticipantId { get; }

        /// <summary>
        /// This value must be between (inclusive) MinPoints and MaxPoints.
        /// </summary>
        PointTotal Points { get; }

        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// </remarks>
        PointTotal MinPoints { get; }

        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// </remarks>
        PointTotal MaxPoints { get; }
    }
}