using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class IdIdDTO : IRelationDTO<Id, int, Id, int>
    {
        public IdIdDTO(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public override IRelationDTO<Id, int, Id, int> Clone()
        {
            return new IdIdDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}