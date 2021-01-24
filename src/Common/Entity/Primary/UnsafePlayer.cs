using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IPlayer"/>
    public class UnsafePlayer : ABCIdNamedEntity, IPlayer
    {
        public UnsafePlayer(Name name, bool isBlocked=false)
            : base (name)
        {
            this.IsBlocked = isBlocked;
        }

        public UnsafePlayer(Id id, Name name, bool isBlocked=false)
            : base (id, name)
        {
            this.IsBlocked = isBlocked;
        }

        public UnsafePlayer(IPlayerDTO dto)
            : base (dto.Id, dto.Name)
        {
            this.IsBlocked = dto.IsBlocked;
        }

        public override object Clone()
        {
            return new PlayerDTO(
                this.Id,
                this.Name,
                this.IsBlocked
            );
        }

        public bool IsBlocked { get; set; }
    }
}