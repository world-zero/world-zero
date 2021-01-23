using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTag"/>
    public class UnsafeMetaTaskTag : ABCTaggedEntity<Id, int>, IMetaTaskTag
    {
        public Id MetaTaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeMetaTaskTag(Id metaTaskId, Name TagId)
            : base(metaTaskId, TagId)
        { }

        public UnsafeMetaTaskTag(Id id, Id metaTaskId, Name TagId)
            : base(id, metaTaskId, TagId)
        { }

        public UnsafeMetaTaskTag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskTag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeMetaTaskTag(IMetaTaskTagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeMetaTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}