using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Dual
{
    public class NameNameDTO : IDualDTO<Name, string, Name, string>
    {
        public NameNameDTO(Name leftName, Name rightName)
            : base(leftName, rightName)
        { }

        public override IDualDTO<Name, string, Name, string> DeepCopy()
        {
            return new NameNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}