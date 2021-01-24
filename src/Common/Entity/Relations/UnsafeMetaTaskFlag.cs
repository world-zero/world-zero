using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskFlag"/>
    public class UnsafeMetaTaskFlag
        : ABCFlaggedEntity<Id, int>,
          IMetaTaskFlag
    {
        public Id MetaTaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeMetaTaskFlag(Id metaTaskId, Name flagId)
            : base(metaTaskId, flagId)
        { }

        public UnsafeMetaTaskFlag(Id id, Id metaTaskId, Name flagId)
            : base(id, metaTaskId, flagId)
        { }

        public UnsafeMetaTaskFlag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskFlag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskFlag(IMetaTaskFlagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override object Clone()
        {
            return new MetaTaskFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}