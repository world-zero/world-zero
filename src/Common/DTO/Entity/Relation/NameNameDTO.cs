using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class NameNameDTO : IRelationDTO<Name, string, Name, string>
    {
        public NameNameDTO(Name leftName, Name rightName)
            : base(leftName, rightName)
        { }

        public override IRelationDTO<Name, string, Name, string> Clone()
        {
            return new NameNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}