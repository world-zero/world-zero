using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IPraxisFlagDTO"/>
    public class PraxisFlagDTO : FlaggedDTO<Id, int>, IPraxisFlagDTO
    {
        public Id PraxisId { get; }

        public PraxisFlagDTO(
            Id id=null,
            Id praxisId=null,
            Name flagId=null
        )
            : base(id, praxisId, flagId)
        { }

        public override object Clone()
        {
            return new PraxisFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as PraxisFlagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}