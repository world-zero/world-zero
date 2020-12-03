using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to a praxis, allowing for multiple
    /// characters to participate on a single praxis.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `CharacterId`
    /// </summary>
    /// <remarks>
    /// A Praxis should always have at least one participant.
    /// <br />
    /// Since some characters can repeat tasks, but since the praxis is
    /// different for every attempt, this does not track the attempt number.
    /// <br />
    /// Since a relation requires a left and right ID to be set on
    /// initialization, `PraxisId` will default to a `new Id(0)` when supplied
    /// with null. Similarly, if `LeftId` contains `new Id(0)`, `PraxisId` will
    /// return null. This will not apply to `LeftId`, it will contain
    /// `new Id(0)` like normal.
    /// </remarks>
    public class PraxisParticipant : IIdIdRelation
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

        public PraxisParticipant(Id characterId)
            : base(new Id(0), characterId)
        { }

        public PraxisParticipant(Id praxisId, Id characterId)
            : base(praxisId, characterId)
        { }

        public PraxisParticipant(
            Id id,
            Id praxisId,
            Id characterId
        )
            : base(id, praxisId, characterId)
        { }

        public PraxisParticipant(RelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public PraxisParticipant(Id id, RelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal PraxisParticipant(
            int id,
            int praxisId,
            int characterId
        )
            : base(new Id(id), new Id(praxisId), new Id(characterId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisParticipant(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}