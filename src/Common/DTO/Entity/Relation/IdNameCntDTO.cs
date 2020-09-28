using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class IdNameCntDTO : ICntRelationDTO<Id, int, Name, string>
    {
        public IdNameCntDTO(Id leftName, Name rightName, int count)
            : base(leftName, rightName, count)
        { }

        public override IRelationDTO<Id, int, Name, string> Clone()
        {
            return new IdNameCntDTO(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}