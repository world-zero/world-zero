using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    public class TaggedDTO<TLeftId, TLeftBuiltIn>
        : RelationDTO<TLeftId, TLeftBuiltIn, Name, string>,
        ITaggedDTO<TLeftId, TLeftBuiltIn>
        where TLeftId : ABCSingleValueObject<TLeftBuiltIn>
    {
        public Name TagId { get { return this.RightId; } }

        public TaggedDTO(Id id=null, TLeftId leftId=null, Name rightId=null)
            : base(id, leftId, rightId)
        { }

        public override object Clone()
        {
            return new TaggedDTO<TLeftId, TLeftBuiltIn>(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}