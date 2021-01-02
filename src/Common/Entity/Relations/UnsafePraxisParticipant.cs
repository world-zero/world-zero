using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipant"/>
    public class UnsafePraxisParticipant
        : ABCEntityRelation<Id, int, Id, int>, IPraxisParticipant
    {
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

        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public UnsafePraxisParticipant(Id characterId)
            : base(new Id(0), characterId)
        { }

        public UnsafePraxisParticipant(Id praxisId, Id characterId)
            : base(praxisId, characterId)
        { }

        public UnsafePraxisParticipant(
            Id id,
            Id praxisId,
            Id characterId
        )
            : base(id, praxisId, characterId)
        { }

        public UnsafePraxisParticipant(RelationDTO<Id, int, Id, int> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafePraxisParticipant(Id id, RelationDTO<Id, int, Id, int> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafePraxisParticipant(
            int id,
            int praxisId,
            int characterId
        )
            : base(new Id(id), new Id(praxisId), new Id(characterId))
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafePraxisParticipant(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override RelationDTO<Id, int, Id, int> GetDTO()
        {
            return new RelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId
            );
        }
    }
}