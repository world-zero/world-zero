using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    public class UnsafeStatus : ABCNamedEntity, IStatus
    {
        public UnsafeStatus(Name id, string description=null)
            : base(id)
        {
            this.Description = description;
        }

        public UnsafeStatus(IStatusDTO dto)
            : base(dto.Id)
        {
            this.Description = dto.Description;
        }

        public override object Clone()
        {
            return new StatusDTO(
                this.Id,
                this.Description
            );
        }

        public string Description { get; set; }
    }
}