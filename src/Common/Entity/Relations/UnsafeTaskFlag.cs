using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Flag's ID,
    /// signifying that the task has flag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public class UnsafeTaskFlag : ABCFlaggedEntity<Id, int>, IUnsafeEntity
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        public Id TaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeTaskFlag(Id taskId, Name flagId)
            : base(taskId, flagId)
        { }

        public UnsafeTaskFlag(Id id, Id taskId, Name flagId)
            : base(id, taskId, flagId)
        { }

        public UnsafeTaskFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeTaskFlag(int id, int taskId, string flagId)
            : base(new Id(id), new Id(taskId), new Name(flagId))
        { }

        public override ABCEntity<Id, int> Clone()
        {
            return new UnsafeTaskFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}