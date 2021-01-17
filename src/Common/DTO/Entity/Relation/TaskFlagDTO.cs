using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="ITaskFlagDTO.cs"/>
    public class TaskFlagDTO : FlaggedDTO<Id, int>, ITaskFlagDTO
    {
        public Id TaskId { get; }

        public TaskFlagDTO(
            Id id=null,
            Id praxisId=null,
            Name flagId=null
        )
            : base(id, praxisId, flagId)
        { }

        public override object Clone()
        {
            return new TaskFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as TaskFlagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}