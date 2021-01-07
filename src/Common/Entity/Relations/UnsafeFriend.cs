using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IFriend"/>
    public class UnsafeFriend : ABCEntitySelfRelation<Id, int>, IFriend
    {
        public override RelationDTO<Id, int, Id, int> GetRelationDTO()
        {
            return new RelationDTO<Id, int, Id, int>(
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

        public UnsafeFriend(RelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeFriend(Id id, RelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeFriend(int id, int firstCharacterId, int secondCharacterId)
            : base(
                new Id(id),
                new Id(firstCharacterId),
                new Id(secondCharacterId)
            )
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