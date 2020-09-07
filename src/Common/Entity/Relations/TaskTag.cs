using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Tag's ID,
    /// signifying that the task has tag X.
    /// </summary>
    public class TaskTag : IIdNameRelation
    {
        /// <summary>
        /// TaskId is a wrapper for RightId.
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

        public TaskTag(IdNameDTO dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public TaskTag(Id id, IdNameDTO dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal TaskTag(int id, int taskId, string tagId)
            : base(new Id(id), new Id(taskId), new Name(tagId))
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}