using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="ITagDTO"/>
    public class TagDTO : EntityDTO<Name, string>, ITagDTO
    {
        public string Description { get; }

        public TagDTO(
            Name id=null,
            string desc=null
        )
            : base(id)
        {
            this.Description = desc;
        }

        public override object Clone()
        {
            return new TagDTO(
                this.Id,
                this.Description
            );
        }

        public override bool Equals(IDTO dto)
        {
            var t = dto as TagDTO;
            return
                t != null
                && base.Equals(t)
                && this.XOR(this.Description, t.Description);
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