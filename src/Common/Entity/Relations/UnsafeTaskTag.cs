using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ITaskTag"/>
    public class UnsafeTaskTag : ABCTaggedEntity<Id, int>, ITaskTag
    {
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

        public UnsafeTaskTag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskTag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        public UnsafeTaskTag(ITaskTagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}