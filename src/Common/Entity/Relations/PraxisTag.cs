using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Praxis's ID to a Tag's ID,
    /// signifying that the praxis has tag X.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public class PraxisTag : IIdNameRelation
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
        /// TagId is a wrapper for RightId.
        /// </summary>
        public Name TagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisTag(Id PraxisId, Name tagId)
            : base(PraxisId, tagId)
        { }

        public PraxisTag(Id id, Id PraxisId, Name tagId)
            : base(id, PraxisId, tagId)
        { }

        public PraxisTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public PraxisTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal PraxisTag(int id, int praxisId, string tagId)
            : base(new Id(id), new Id(praxisId), new Name(tagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}