using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IFriend"/>
    public sealed class Friend
        : IUnsafeSelfRelationProxy<UnsafeFriend, Id, int>, IFriend
    {
        public Friend(UnsafeFriend friend)
            : base(friend)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Friend(this._unsafeEntity);
        }

        public Id FirstCharacterId => this._unsafeEntity.FirstCharacterId;
        public Id SecondCharacterId => this._unsafeEntity.SecondCharacterId;
    }
}