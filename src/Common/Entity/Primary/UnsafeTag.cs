using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ITag"/>
    public class UnsafeTag : ABCNamedEntity, ITag
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public UnsafeTag(Name name)
            : base(name)
        { }

        public UnsafeTag(ITagDTO dto)
            : base(dto.Id)
        {
            this.Description = dto.Description;
        }

        public override object Clone()
        {
            return new TagDTO(this.Id, this.Description);
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }
    }
}
