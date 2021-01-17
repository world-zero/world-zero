using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTagDTO"/>
    public class PraxisTagDTO : TaggedDTO<Id, int>, IPraxisTagDTO
    {
        public Id PraxisId { get; }

        public PraxisTagDTO(
            Id id=null,
            Id mtId=null,
            Name tagId=null
        )
            : base(id, mtId, tagId)
        { }

        public override object Clone()
        {
            return new PraxisTagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as PraxisTagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}