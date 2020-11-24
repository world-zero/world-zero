using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a MetaTask's ID to a Flag's ID,
    /// signifying that the meta task has flag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public class MetaTaskFlag : IIdNameRelation
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        public Id MetaTaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// FlagId is a wrapper for RightId.
        /// </summary>
        public Name FlagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public MetaTaskFlag(Id metaTaskId, Name flagId)
            : base(metaTaskId, flagId)
        { }

        public MetaTaskFlag(Id id, Id metaTaskId, Name flagId)
            : base(id, metaTaskId, flagId)
        { }

        public MetaTaskFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public MetaTaskFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        internal MetaTaskFlag(int id, int metaTaskId, string flagId)
            : base(new Id(id), new Id(metaTaskId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTaskFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}