using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Tag's ID,
    /// signifying that the task has tag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public class UnsafeTaskTag : ITaggedEntity<Id, int>, IUnsafeEntity
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        public Id TaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeTaskTag(Id taskId, Name tagId)
            : base(taskId, tagId)
        { }

        public UnsafeTaskTag(Id id, Id taskId, Name tagId)
            : base(id, taskId, tagId)
        { }

        public UnsafeTaskTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeTaskTag(int id, int taskId, string tagId)
            : base(new Id(id), new Id(taskId), new Name(tagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}