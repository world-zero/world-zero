using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Dual
{
    public class IdIdDTO : IDualDTO<Id, int, Id, int>
    {
        public IdIdDTO(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public override IDualDTO<Id, int, Id, int> DeepCopy()
        {
            return new IdIdDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}