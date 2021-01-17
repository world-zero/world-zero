using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskFlagDTO"/>
    public class MetaTaskFlagDTO : FlaggedDTO<Id, int>, IMetaTaskFlagDTO
    {
        public Id MetaTaskId { get; }

        public MetaTaskFlagDTO(
            Id id=null,
            Id mtId=null,
            Name flagId=null
        )
            : base(id, mtId, flagId)
        { }

        public override object Clone()
        {
            return new MetaTaskFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as MetaTaskFlagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}