using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are collaberating on a.
    /// </summary>
    public class PraxisParticipant : IIdIdCntRelation
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// CharacterId wraps LeftId, which is the ID of the related Character.
        /// </summary>
        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisParticipant(Id praxisId, Id characterId, int count=1)
            : base(praxisId, characterId, count)
        { }

        public PraxisParticipant(
            Id id,
            Id praxisId,
            Id characterId,
            int count=1
        )
            : base(id, praxisId, characterId, count)
        { }

        public PraxisParticipant(IdIdDTO dto, int count=1)
            : base(dto.LeftId, dto.RightId, count)
        { }

        public PraxisParticipant(Id id, IdIdDTO dto, int count=1)
            : base(id, dto.LeftId, dto.RightId, count)
        { }

        internal PraxisParticipant(
            int id,
            int praxisId,
            int characterId,
            int count
        )
            : base(new Id(id), new Id(praxisId), new Id(characterId), count)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisParticipant(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}