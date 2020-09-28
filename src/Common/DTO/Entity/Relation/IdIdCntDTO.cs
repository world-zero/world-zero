using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class IdIdCntDTO : ICntRelationDTO<Id, int, Id, int>
    {
        public IdIdCntDTO(Id leftId, Id rightId, int count)
            : base(leftId, rightId, count)
        { }

        public override IRelationDTO<Id, int, Id, int> Clone()
        {
            return new IdIdCntDTO(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}