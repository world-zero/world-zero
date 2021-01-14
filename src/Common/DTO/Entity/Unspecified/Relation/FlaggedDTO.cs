using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    public class FlaggedDTO<TLeftId, TLeftBuiltIn>
        : RelationDTO<TLeftId, TLeftBuiltIn, Name, string>,
        IFlaggedDTO<TLeftId, TLeftBuiltIn>
        where TLeftId : ABCSingleValueObject<TLeftBuiltIn>
    {
        public Name FlagId { get { return this.RightId; } }

        public FlaggedDTO(Id id=null, TLeftId leftId=null, Name rightId=null)
            : base(id, leftId, rightId)
        { }

        public override object Clone()
        {
            return new FlaggedDTO<TLeftId, TLeftBuiltIn>(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}