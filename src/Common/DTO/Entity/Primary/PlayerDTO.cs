using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IPlayerDTO"/>
    public class PlayerDTO : IdNamedDTO, IPlayerDTO
    {
        public bool IsBlocked { get; }

        public PlayerDTO(
            Id id=null,
            Name name=null,
            bool isBlocked=false
        )
            : base(id, name)
        {
            this.IsBlocked = isBlocked;
        }

        public override object Clone()
        {
            return new PlayerDTO(
                this.Id,
                this.Name,
                this.IsBlocked
            );
        }

        public override bool Equals(IDTO dto)
        {
            var p = dto as PlayerDTO;
            return
                p != null
                && base.Equals(p)
                && this.XOR(this.IsBlocked, p.IsBlocked);
        }

        // No GetHashCode override is performed.
    }
}