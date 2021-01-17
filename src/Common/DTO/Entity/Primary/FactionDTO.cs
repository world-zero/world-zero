using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IFactionDTO"/>
    public class FactionDTO : EntityDTO<Name, string>, IFactionDTO
    {
        public string Description { get; }
        public PastDate DateFounded { get; }
        public Name AbilityId { get; }

        public FactionDTO(
            Name id=null,
            string desc=null,
            PastDate dateFounded=null,
            Name abilityId=null
        )
            : base(id)
        {
            this.Description = desc;
            this.DateFounded = dateFounded;
            this.AbilityId = abilityId;
        }

        public override object Clone()
        {
            return new FactionDTO(
                this.Id,
                this.Description,
                this.DateFounded,
                this.AbilityId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var f = dto as FactionDTO;
            return
                f != null
                && base.Equals(f)
                && this.XOR(f.Description, this.Description)
                && this.XOR(f.DateFounded, this.DateFounded)
                && this.XOR(f.AbilityId, this.AbilityId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Description != null) r *= this.Description.GetHashCode();
                if (this.DateFounded != null) r *= this.DateFounded.GetHashCode();
                if (this.AbilityId != null) r *= this.AbilityId.GetHashCode();
                return r;
            }
        }
    }
}