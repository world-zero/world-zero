using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IFriend"/>
    public class UnsafeFriend : ABCEntitySelfRelation<Id, int>, IFriend
    {
        public override NoIdRelationDTO<Id, int, Id, int> GetNoIdRelationDTO()
        {
            return new NoIdRelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId
            );
        }

        public Id FirstCharacterId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public Id SecondCharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public UnsafeFriend(Id firstCharacterId, Id secondCharacterId)
            : base(firstCharacterId, secondCharacterId)
        { }

        public UnsafeFriend(Id id, Id firstCharacterId, Id secondCharacterId)
            : base(id, firstCharacterId, secondCharacterId)
        { }

        public UnsafeFriend(NoIdRelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeFriend(Id id, NoIdRelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        public UnsafeFriend(IFriendDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeFriend(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}