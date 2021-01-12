using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are friends.
    /// <br />
    /// Left relation: `FirstCharacterId`
    /// <br />
    /// Right relation: `SecondCharacterId`
    /// </summary>
    public interface IFriendDTO : IEntitySelfRelationDTO<Id, int>
    {
        /// <summary>
        /// FirstCharacterId is a wrapper for LeftId.
        /// </summary>
        Id FirstCharacterId { get; }

        /// <summary>
        /// SecondCharacterId is a wrapper for RightId.
        /// </summary>
        Id SecondCharacterId { get; }
    }
}