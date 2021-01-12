using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IFriendDTO"/>
    public interface IFriend : IEntitySelfRelation<Id, int>
    {
        Id FirstCharacterId { get; }
        Id SecondCharacterId { get; }
    }
}