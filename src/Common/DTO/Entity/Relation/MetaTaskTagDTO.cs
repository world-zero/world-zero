using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTagDTO"/>
    public class MetaTaskTagDTO : TaggedDTO<Id, int>, IMetaTaskTagDTO
    {
        public Id MetaTaskId { get; }

        public MetaTaskTagDTO(
            Id id=null,
            Id mtId=null,
            Name tagId=null
        )
            : base(id, mtId, tagId)
        { }

        public override object Clone()
        {
            return new MetaTaskTagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as MetaTaskTagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}