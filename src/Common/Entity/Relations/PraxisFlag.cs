using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Praxis's ID to a Flag's ID,
    /// signifying that the praxis has flag X.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public class PraxisFlag : IIdNameRelation
    {
        /// <summary>
        /// PraxisId is a wrapper for LeftId.
        /// </summary>
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// FlagId is a wrapper for RightId.
        /// </summary>
        public Name FlagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisFlag(Id praxisId, Name flagId)
            : base(praxisId, flagId)
        { }

        public PraxisFlag(Id id, Id praxisId, Name flagId)
            : base(id, praxisId, flagId)
        { }

        public PraxisFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public PraxisFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal PraxisFlag(int id, int praxisId, string flagId)
            : base(new Id(id), new Id(praxisId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}