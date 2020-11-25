using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Meta Task's ID to a Tag's ID,
    /// signifying that the meta task has tag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public class MetaTaskTag : IIdNameRelation
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        public Id MetaTaskId
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

        public MetaTaskTag(Id metaTaskId, Name TagId)
            : base(metaTaskId, TagId)
        { }

        public MetaTaskTag(Id id, Id metaTaskId, Name TagId)
            : base(id, metaTaskId, TagId)
        { }

        public MetaTaskTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public MetaTaskTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal MetaTaskTag(int id, int metaTaskId, string tagId)
            : base(new Id(id), new Id(metaTaskId), new Name(tagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}