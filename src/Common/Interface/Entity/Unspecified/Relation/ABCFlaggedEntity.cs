using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IFlaggedEntity"/>
    public abstract class ABCFlaggedEntity<TLeftId, TLeftBuiltIn>
        : ABCEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
          IFlaggedEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
    {
        public ABCFlaggedEntity(TLeftId leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public ABCFlaggedEntity(Id id, TLeftId leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

        public Name FlagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public override
        NoIdRelationDTO<TLeftId, TLeftBuiltIn, Name, string> GetRelationDTO()
        {
            return new NoIdRelationDTO<TLeftId, TLeftBuiltIn, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}