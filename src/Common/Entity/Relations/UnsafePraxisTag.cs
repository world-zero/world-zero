using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTag"/>
    public class UnsafePraxisTag
        : ABCTaggedEntity<Id, int>, IPraxisTag
    {
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafePraxisTag(Id PraxisId, Name tagId)
            : base(PraxisId, tagId)
        { }

        public UnsafePraxisTag(Id id, Id PraxisId, Name tagId)
            : base(id, PraxisId, tagId)
        { }

        public UnsafePraxisTag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafePraxisTag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        public UnsafePraxisTag(IPraxisTagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override object Clone()
        {
            return new PraxisTagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}