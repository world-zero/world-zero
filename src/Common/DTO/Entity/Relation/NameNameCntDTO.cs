using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.DTO.Entity.Relation
{
    public class NameNameCntDTO : ICntRelationDTO<Name, string, Name, string>
    {
        public NameNameCntDTO(Name leftName, Name rightName, int count)
            : base(leftName, rightName, count)
        { }

        public override IRelationDTO<Name, string, Name, string> Clone()
        {
            return new NameNameCntDTO(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}