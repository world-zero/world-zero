using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ITaskFlag"/>
    public class UnsafeTaskFlag
        : IUnsafeFlaggedEntity<Id, int>, ITaskFlag
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

        public UnsafeTaskFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeTaskFlag(int id, int taskId, string flagId)
            : base(new Id(id), new Id(taskId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeTaskFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}