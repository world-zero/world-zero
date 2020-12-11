using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="ITaggedEntity"/>
    public abstract class ABCTaggedEntity<TLeftId, TLeftBuiltIn>
        : ABCEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
          ITaggedEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
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
        RelationDTO<TLeftId, TLeftBuiltIn, Name, string> GetDTO()
        {
            return new RelationDTO<TLeftId, TLeftBuiltIn, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}