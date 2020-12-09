using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisFlag"/>
    public class UnsafePraxisFlag
        : UnsafeIFlaggedEntity<Id, int>, IPraxisFlag
    {
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafePraxisFlag(Id praxisId, Name flagId)
            : base(praxisId, flagId)
        { }

        public UnsafePraxisFlag(Id id, Id praxisId, Name flagId)
            : base(id, praxisId, flagId)
        { }

        public UnsafePraxisFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafePraxisFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafePraxisFlag(int id, int praxisId, string flagId)
            : base(new Id(id), new Id(praxisId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafePraxisFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}