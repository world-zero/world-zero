using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="ICommentDTO"/>
    public class CommentDTO : CntRelationDTO<Id, int, Id, int>, ICommentDTO
    {
        public Id PraxisId { get; }
        public Id CharacterId { get; }
        public string Value { get; }

        public CommentDTO(
            Id id=null,
            Id praxisId=null,
            Id charId=null,
            int count=0,
            string value=null
        )
            : base(id, praxisId, charId, count)
        {
            this.Value = value;
        }

        public override object Clone()
        {
            return new CommentDTO(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Count,
                this.Value
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as CommentDTO;
            return
                c != null
                && base.Equals(c)
                && this.XOR(this.Value, c.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Value != null) r *= this.Value.GetHashCode();
                return r;
            }
        }
    }
}