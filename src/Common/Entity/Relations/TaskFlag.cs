using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Flag's ID,
    /// signifying that the task has flag X.
    /// </summary>
    public class TaskFlag : IIdNameRelation
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
        /// FlagId is a wrapper for RightId.
        /// </summary>
        public Name FlagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public TaskFlag(Id taskId, Name flagId)
            : base(taskId, flagId)
        { }

        public TaskFlag(Id id, Id taskId, Name flagId)
            : base(id, taskId, flagId)
        { }

        public TaskFlag(IdNameDTO dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public TaskFlag(Id id, IdNameDTO dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal TaskFlag(int id, int taskId, string flagId)
            : base(new Id(id), new Id(taskId), new Name(flagId))
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TaskFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}