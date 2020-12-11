using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

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

        public UnsafeMetaTaskFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal UnsafeMetaTaskFlag(int id, int metaTaskId, string flagId)
            : base(new Id(id), new Id(metaTaskId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeMetaTaskFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}