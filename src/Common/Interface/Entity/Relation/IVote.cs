using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IIdIdRelation"/>
    /// <summary>
    /// This relation applies a vote from the supplied character to the
    /// corresponding praxis participant.
    /// <br />
    /// Left relation: `CharacterId` of the character submitting the vote.
    /// <br />
    /// Right relation: `PraxisParticipantId`
    /// </summary>
    /// <remarks>
    /// While making sure that a player isn't voting on their own character
    /// requires knowing all of the player's character IDs, which is firmly the
    /// responsiblity of VoteReg.
    /// <br />
    /// Check out the documentation on `StaticMinPoints` and `StaticMaxPoints`.
    /// </remarks>
    public interface IVote : IEntityRelation<Id, int, Id, int>
    {
        /// <summary>
        /// CharacterId is a wrapper for LeftId.
        /// </summary>
        Id CharacterId { get; }

        /// <summary>
        /// PraxisParticipantId is a wrapper for RightId.
        /// </summary>
        Id PraxisParticipantId { get; }

        /// <summary>
        /// This value must be between (inclusive) MinPoints and MaxPoints.
        /// </summary>
        PointTotal Points { get; }

        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// <br />
        /// There will be a static property `MnPoints` that this will be a
        /// wrapper for.
        /// </remarks>
        PointTotal MinPoints { get; }

        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// <br />
        /// There will be a static property `MaxPoints` that this will be a
        /// wrapper for.
        /// </remarks>
        PointTotal MaxPoints { get; }
    }
}