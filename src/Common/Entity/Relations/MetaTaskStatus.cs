using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Meta Task's ID to a Status' ID,
    /// signifying that the meta task has status X.
    /// </summary>
    public class MetaTaskStatus : IIdNameRelation
    {
        /// <summary>
        /// MetaTaskId is a wrapper for RightId.
        /// </summary>
        public Id MetaTaskId
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

        public MetaTaskStatus(Id metaTaskId, Name StatusId)
            : base(metaTaskId, StatusId)
        { }

        public MetaTaskStatus(Id id, Id metaTaskId, Name StatusId)
            : base(id, metaTaskId, StatusId)
        { }

        public MetaTaskStatus(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public MetaTaskStatus(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal MetaTaskStatus(int id, int metaTaskId, string statusId)
            : base(new Id(id), new Id(metaTaskId), new Name(statusId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTaskStatus(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}