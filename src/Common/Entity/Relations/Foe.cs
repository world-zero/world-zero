using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are foes.
    /// </summary>
    public class Foe : IIdIdRelation
    {
        /// <summary>
        /// FirstCharacterId is a wrapper for RightId.
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

        public Foe(Id firstCharacterId, Id secondCharacterId)
            : base(firstCharacterId, secondCharacterId)
        { }

        public Foe(Id id, Id firstCharacterId, Id secondCharacterId)
            : base(id, firstCharacterId, secondCharacterId)
        { }

        public Foe(IdIdDTO dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public Foe(Id id, IdIdDTO dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal Foe(int id, int firstCharacterId, int secondCharacterId)
            : base
            (
                new Id(id),
                new Id(firstCharacterId),
                new Id(secondCharacterId)
            )
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Foe(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}