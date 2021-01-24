using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ITaskFlag"/>
    public class UnsafeTaskFlag
        : ABCFlaggedEntity<Id, int>, ITaskFlag
    {
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

        public UnsafeTaskFlag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskFlag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskFlag(ITaskFlagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override object Clone()
        {
            return new TaskFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}