using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IFlaggedEntity"/>
    public abstract class UnsafeIFlaggedEntity<TLeftId, TLeftBuiltIn>
        : UnsafeIEntityRelation<TLeftId, TLeftBuiltIn, Name, string>,
          IFlaggedEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
    {
        public UnsafeIFlaggedEntity(TLeftId leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public UnsafeIFlaggedEntity(Id id, TLeftId leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

        public Name FlagId
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