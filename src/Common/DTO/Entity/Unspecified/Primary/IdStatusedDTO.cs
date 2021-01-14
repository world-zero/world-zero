using WorldZero.Common.Interface.DTO;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Unspecified.Primary
{
    /// <inheritdoc cref="IEntityDTO{TId, TBuiltIn}"/>
    public class IdStatusedDTO : EntityDTO<Id, int>, IIdStatusedDTO
    {
        public Name StatusId { get; private set; }

        public IdStatusedDTO(Id id, Name statusId)
            : base(id)
        {
            this.StatusId = statusId;
        }

        public override object Clone()
        {
            return new IdStatusedDTO(this.Id, this.StatusId);
        }

        public override bool Equals(IDTO dto)
        {
            var idStatused = dto as IdStatusedDTO;
            if (idStatused == null)                   return false;
            if (idStatused.StatusId != this.StatusId) return false;
            return base.Equals(idStatused);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (this.StatusId == null)
                    return base.GetHashCode();
                else
                    return base.GetHashCode() * this.StatusId.GetHashCode();
            }
        }
    }
}