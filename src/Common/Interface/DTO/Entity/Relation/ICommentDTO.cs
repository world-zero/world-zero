using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a praxis' ID to a character's ID, as well as
    /// containing the content of the comment they are leaving on the related
    /// praxis.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `CharacterId`
    /// </summary>
    public interface ICommentDTO : IEntityCntRelationDTO<Id, int, Id, int>
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        Id PraxisId { get; }

        /// <summary>
        /// CharacterId wraps RightId, which is the ID of the related Character.
        /// </summary>
        Id CharacterId { get; } 

        /// <summary>
        /// This is the text of the comment.
        /// </summary>
        string Value { get; }
    }
}