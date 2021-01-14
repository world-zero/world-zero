using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    public class SelfRelationDTO<TId, TBuiltIn>
        : RelationDTO<TId, TBuiltIn, TId, TBuiltIn>,
        IEntitySelfRelationDTO<TId, TBuiltIn>
        where TId  : ABCSingleValueObject<TBuiltIn>
    {
        public SelfRelationDTO(
            Id id=null,
            TId leftId=null,
            TId rightId=null
        ) : base(id, leftId, rightId)
        { }

        public override object Clone()
        {
            return new SelfRelationDTO<TId, TBuiltIn>(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}