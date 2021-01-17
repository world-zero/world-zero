using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipantDTO"/>
    public class PraxisParticipantDTO
        : RelationDTO<Id, int, Id, int>,
        IPraxisParticipantDTO
    {
        public Id PraxisId { get; }
        public Id CharacterId { get; }

        public PraxisParticipantDTO(
            Id id=null,
            Id praxisId=null,
            Id charId=null
        )
            : base(id, praxisId, charId)
        { }

        public override object Clone()
        {
            return new PraxisParticipantDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as PraxisParticipantDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}