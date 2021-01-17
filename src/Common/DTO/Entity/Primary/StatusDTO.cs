using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="ABCEntityDTO{TId, TBuiltIn}"/>
    public class StatusDTO : EntityDTO<Name, string>, IStatusDTO
    {
        public string Description { get; }

        public StatusDTO(
            Name id=null,
            string desc=null
        )
            : base(id)
        {
            this.Description = desc;
        }

        public override object Clone()
        {
            return new StatusDTO(
                this.Id,
                this.Description
            );
        }

        public override bool Equals(IDTO dto)
        {
            var s = dto as StatusDTO;
            return
                s != null
                && base.Equals(dto)
                && this.XOR(this.Description, s.Description);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Description != null)
                    r *= this.Description.GetHashCode();
                return r;
            }
        }
    }
}