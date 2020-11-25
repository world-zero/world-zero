using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// <remarks>
    /// This will configure the right ID to be a Name, intended to be used as a
    /// relation to a Flag.
    /// </remarks>
    public abstract class IFlaggedEntity<TLeftId, TLeftBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, Name, string>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
    {
        public IFlaggedEntity(TLeftId leftId, Name tagId)
            : base(leftId, tagId)
        { }

        public IFlaggedEntity(Id id, TLeftId leftId, Name tagId)
            : base(id, leftId, tagId)
        { }

        /// <summary>
        /// FlagId is a wrapper for RightId.
        /// </summary>
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