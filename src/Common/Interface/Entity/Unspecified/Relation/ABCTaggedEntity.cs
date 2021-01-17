using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="ITaggedEntity"/>
    public abstract class ABCTaggedEntity<TLeftId, TLeftBuiltIn>
        : ABCEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
          ITaggedEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    {
        public ABCTaggedEntity(TLeftId leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public ABCTaggedEntity(Id id, TLeftId leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

        /// <summary>
        /// TagId is a wrapper for RightId.
        /// </summary>
        public Name TagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public override
        NoIdRelationDTO<TLeftId, TLeftBuiltIn, Name, string> GetNoIdRelationDTO()
        {
            return new NoIdRelationDTO<TLeftId, TLeftBuiltIn, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}