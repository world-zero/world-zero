using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntitySelfRelation"/>
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are friends.
    /// <br />
    /// Left relation: `FirstCharacterId`
    /// <br />
    /// Right relation: `SecondCharacterId`
    /// </summary>
    public interface IFriend : IEntitySelfRelation<Id, int>
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