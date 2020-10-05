using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Praxis' ID to a Status' ID,
    /// signifying that the praxis has status X.
    /// </summary>
    public class PraxisStatus : IIdNameRelation
    {
        /// <summary>
        /// PraxisId is a wrapper for RightId.
        /// </summary>
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// StatusId is a wrapper for RightId.
        /// </summary>
        public Name StatusId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisStatus(Id praxisId, Name StatusId)
            : base(praxisId, StatusId)
        { }

        public PraxisStatus(Id id, Id praxisId, Name StatusId)
            : base(id, praxisId, StatusId)
        { }

        public PraxisStatus(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public PraxisStatus(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal PraxisStatus(int id, int praxisId, string statusId)
            : base(new Id(id), new Id(praxisId), new Name(statusId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisStatus(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}