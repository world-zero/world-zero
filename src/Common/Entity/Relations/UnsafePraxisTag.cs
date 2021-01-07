using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
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

        public UnsafePraxisTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafePraxisTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafePraxisTag(int id, int praxisId, string tagId)
            : base(new Id(id), new Id(praxisId), new Name(tagId))
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafePraxisTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}