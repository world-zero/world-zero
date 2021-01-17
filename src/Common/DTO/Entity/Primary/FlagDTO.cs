using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IFlagDTO"/>
    public class FlagDTO : EntityDTO<Name, string>, IFlagDTO
    {
        public string Description { get; }
        public bool IsFlatPenalty { get; }
        public PointTotal Penalty { get; }

        public FlagDTO(
            Name id=null,
            string desc=null,
            bool isFlatPenalty=true,
            PointTotal penalty=null
        )
            : base(id)
        {
            this.Description = desc;
            this.IsFlatPenalty = isFlatPenalty;
            this.Penalty = penalty;
        }

        public override object Clone()
        {
            return new FlagDTO(
                this.Id,
                this.Description,
                this.IsFlatPenalty,
                this.Penalty
            );
        }

        public override bool Equals(IDTO dto)
        {
            var f = dto as FlagDTO;
            return
                f != null
                && base.Equals(f)
                && this.XOR(this.Description, f.Description)
                && this.XOR(this.IsFlatPenalty, f.IsFlatPenalty)
                && this.XOR(this.Penalty, f.Penalty);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int r = base.GetHashCode();
                if (this.Description != null) r *= this.Description.GetHashCode();
                if (this.Penalty != null) r *= this.Penalty.GetHashCode();
                return r;
            }
        }
    }
}