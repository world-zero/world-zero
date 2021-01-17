using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IFoeDTO"/>
    public class FoeDTO : SelfRelationDTO<Id, int>, IFoeDTO
    {
        public Id FirstCharacterId  { get; }
        public Id SecondCharacterId { get; }

        public FoeDTO(
            Id id=null,
            Id charId0=null,
            Id charId1=null
        )
            : base(id, charId0, charId1)
        { }

        public override object Clone()
        {
            return new FoeDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as FoeDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}