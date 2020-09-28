using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class IdNameDTO : IRelationDTO<Id, int, Name, string>
    {
        public IdNameDTO(Id leftName, Name rightName)
            : base(leftName, rightName)
        { }

        public override IRelationDTO<Id, int, Name, string> Clone()
        {
            return new IdNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}