using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IVoteDTO"/>
    public class VoteDTO : RelationDTO<Id, int, Id, int>, IVoteDTO
    {
        public Id CharacterId { get; }
        public Id PraxisParticipantId { get; }
        public PointTotal Points { get; }

        public VoteDTO(
            Id id=null,
            Id charId=null,
            Id ppId=null,
            PointTotal points=null
        )
            : base(id, charId, ppId)
        {
            this.Points = points;
        }

        public override object Clone()
        {
            return new VoteDTO(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Points
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as VoteDTO;
            return
                c != null
                && base.Equals(c)
                && this.XOR(this.Points, c.Points);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Points != null) r *= this.Points.GetHashCode();
                return r;
            }
        }
    }
}