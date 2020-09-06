using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Dual
{
    public class IdNameDTO : IDualDTO<Id, int, Name, string>
    {
        public IdNameDTO(Id leftName, Name rightName)
            : base(leftName, rightName)
        { }

        public override IDualDTO<Id, int, Name, string> DeepCopy()
        {
            return new IdNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}