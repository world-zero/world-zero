using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are friends.
    /// <br />
    /// Left relation: `FirstCharacterId`
    /// <br />
    /// Right relation: `SecondCharacterId`
    /// </summary>
    public class UnsafeFriend : UnsafeIEntitySelfRelation<Id, int>, IUnsafeEntity
    {
        public override RelationDTO<Id, int, Id, int> GetDTO()
        {
            return new RelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId
            );
        }

        /// <summary>
        /// FirstCharacterId is a wrapper for LeftId.
        /// </summary>
        public Id FirstCharacterId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// SecondCharacterId is a wrapper for RightId.
        /// </summary>
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

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeFriend(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}