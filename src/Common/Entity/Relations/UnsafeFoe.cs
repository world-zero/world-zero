using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are foes.
    /// <br />
    /// Left relation: `FirstCharacterId`
    /// <br />
    /// Right relation: `SecondCharacterId`
    /// </summary>
    public class UnsafeFoe : ABCEntitySelfRelation<Id, int>, IUnsafeEntity
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

        public UnsafeFoe(Id firstCharacterId, Id secondCharacterId)
            : base(firstCharacterId, secondCharacterId)
        { }

        public UnsafeFoe(Id id, Id firstCharacterId, Id secondCharacterId)
            : base(id, firstCharacterId, secondCharacterId)
        { }

        public UnsafeFoe(RelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeFoe(Id id, RelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeFoe(int id, int firstCharacterId, int secondCharacterId)
            : base
            (
                new Id(id),
                new Id(firstCharacterId),
                new Id(secondCharacterId)
            )
        { }

        public override ABCEntity<Id, int> Clone()
        {
            return new UnsafeFoe(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}