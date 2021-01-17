using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IMetaTaskDTO"/>
    public class MetaTaskDTO : IdStatusedDTO, IMetaTaskDTO
    {
        public string Description { get; }
        public bool IsFlatBonus { get; }
        public PointTotal Bonus { get; }
        public Name FactionId { get; }

        public MetaTaskDTO(
            Id id=null,
            Name statusId=null,
            string desc=null,
            bool isFlatBonus=true,
            PointTotal bonus=null,
            Name factionId=null
        )
            : base(id, statusId)
        {
            this.Description = desc;
            this.IsFlatBonus = isFlatBonus;
            this.Bonus = bonus;
            this.FactionId = factionId;
        }

        public override object Clone()
        {
            return new MetaTaskDTO(
                this.Id,
                this.StatusId,
                this.Description,
                this.IsFlatBonus,
                this.Bonus,
                this.FactionId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var mt = dto as MetaTaskDTO;
            return
                mt != null
                && base.Equals(mt)
                && this.XOR(this.Description, mt.Description)
                && this.XOR(this.IsFlatBonus, mt.IsFlatBonus)
                && this.XOR(this.Bonus, mt.Bonus)
                && this.XOR(this.FactionId, mt.FactionId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Description != null) r *= this.Description.GetHashCode();
                if (this.Bonus != null) r *= this.Bonus.GetHashCode();
                if (this.FactionId != null) r *= this.FactionId.GetHashCode();
                return r;
            }
        }
    }
}