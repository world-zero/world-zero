using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IAbilityDTO"/>
    public class AbilityDTO : EntityDTO<Name, string>, IAbilityDTO
    {
        public string Description { get; private set; }

        public AbilityDTO(
            Name id=null,
            string desc=null
        )
            : base(id)
        {
            this.Description = desc;
        }

        public override object Clone()
        {
            return new AbilityDTO(this.Id, this.Description);
        }


        public override bool Equals(IDTO dto)
        {
            AbilityDTO a = dto as AbilityDTO;
            return
                a != null
                && base.Equals(a)
                && this.XOR(a.Description, this.Description);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (this.Description != null)
                    return base.GetHashCode() * this.Description.GetHashCode();
                return base.GetHashCode();
            }
        }
    }
}