using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
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
    /// </remarks>
    public interface IVoteDTO : IEntityRelationDTO<Id, int, Id, int>
    {
        /// <summary>
        /// CharacterId is a wrapper for LeftId.
        /// </summary>
        Id CharacterId { get; }

        /// <summary>
        /// PraxisParticipantId is a wrapper for RightId.
        /// </summary>
        Id PraxisParticipantId { get; }

        PointTotal Points { get; }
        PointTotal MinPoints { get; }
        PointTotal MaxPoints { get; }
    }
}