using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
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
    /// Some characters can repeat tasks, but since the praxis is different for
    /// every attempt, this does not need to track the attempt number.
    /// </remarks>
    public interface IPraxisParticipantDTO
        : IEntityRelationDTO<Id, int, Id, int>
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        Id PraxisId { get; }

        /// <summary>
        /// CharacterId wraps RightId, which is the ID of the related Character.
        /// </summary>
        Id CharacterId { get; }
    }
}