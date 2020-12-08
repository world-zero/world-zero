using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
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
    public class UnsafeMetaTaskTag : ABCTaggedEntity<Id, int>, IUnsafeEntity
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        public Id MetaTaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeMetaTaskTag(Id metaTaskId, Name TagId)
            : base(metaTaskId, TagId)
        { }

        public UnsafeMetaTaskTag(Id id, Id metaTaskId, Name TagId)
            : base(id, metaTaskId, TagId)
        { }

        public UnsafeMetaTaskTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal UnsafeMetaTaskTag(int id, int metaTaskId, string tagId)
            : base(new Id(id), new Id(metaTaskId), new Name(tagId))
        { }

        public override ABCEntity<Id, int> Clone()
        {
            return new UnsafeMetaTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}