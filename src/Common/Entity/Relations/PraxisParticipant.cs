using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to a praxis, allowing for multiple
    /// characters to participate on a single praxis.
    /// </summary>
    /// <remarks>
    /// Since a relation requires a left and right ID to be set on
    /// initialization, `PraxisId` will default to a `new Id(0)` when supplied
    /// with null. Similarly, if `LeftId` contains `new Id(0)`, `PraxisId` will
    /// return null. This will not apply to `LeftId`, it will contain
    /// `new Id(0)` like normal.
    /// </remarks>
    public class PraxisParticipant : IIdIdCntRelation
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        /// <remarks>
        /// Since a relation requires a left and right ID to be set on
        /// initialization, `PraxisId` will default to a `new Id(0)` when
        /// supplied with null. Similarly, if `LeftId` contains `new Id(0)`,
        /// `PraxisId` will return null. This will not apply to `LeftId`, it
        /// will contain `new Id(0)` like normal.
        /// </remarks>
        public Id PraxisId
        {
            get
            {
                if (this.LeftId == new Id(0))
                    return null;
                return this.LeftId;
            }
            set
            {
                if (value == null)
                    this.LeftId = new Id(0);
                else
                    this.LeftId = value;
            }
        }

        /// <summary>
        /// CharacterId wraps RightId, which is the ID of the related Character.
        /// </summary>
        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisParticipant(Id characterId, int count=1)
            : base(new Id(0), characterId, count)
        { }

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

        public PraxisParticipant(CntRelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId, dto.Count)
        { }

        public PraxisParticipant(Id id, CntRelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId, dto.Count)
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