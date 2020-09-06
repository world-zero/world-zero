using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a Meta Task's ID to a Tag's ID,
    /// signifying that the meta task has tag X.
    /// </summary>
    public class MetaTaskTag : IIdNameRelation
    {
        /// <summary>
        /// MetaTaskId is a wrapper for RightId.
        /// </summary>
        public Id MetaTaskId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// TagId is a wrapper for RightId.
        /// </summary>
        public Name TagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public MetaTaskTag(Id metaTaskId, Name TagId)
            : base(metaTaskId, TagId)
        { }

        public MetaTaskTag(Id id, Id metaTaskId, Name TagId)
            : base(id, metaTaskId, TagId)
        { }

        internal MetaTaskTag(int id, int metaTaskId, string tagId)
            : base(new Id(id), new Id(metaTaskId), new Name(tagId))
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new MetaTaskTag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}