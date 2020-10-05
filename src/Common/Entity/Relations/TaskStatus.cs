using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Status' ID,
    /// signifying that the task has status X.
    /// </summary>
    public class TaskStatus : IIdNameRelation
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
        /// StatusId is a wrapper for RightId.
        /// </summary>
        public Name StatusId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public TaskStatus(Id taskId, Name StatusId)
            : base(taskId, StatusId)
        { }

        public TaskStatus(Id id, Id taskId, Name StatusId)
            : base(id, taskId, StatusId)
        { }

        public TaskStatus(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public TaskStatus(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal TaskStatus(int id, int taskId, string statusId)
            : base(new Id(id), new Id(taskId), new Name(statusId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new TaskStatus(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}