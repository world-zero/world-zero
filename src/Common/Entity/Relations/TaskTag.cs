using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
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
    public class TaskTag : IIdNameRelation
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        public Id TaskId
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

        public TaskTag(Id taskId, Name tagId)
            : base(taskId, tagId)
        { }

        public TaskTag(Id id, Id taskId, Name tagId)
            : base(id, taskId, tagId)
        { }

        public TaskTag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public TaskTag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal TaskTag(int id, int taskId, string tagId)
            : base(new Id(id), new Id(taskId), new Name(tagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new TaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}