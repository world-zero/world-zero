using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Status is a entity for a tuple of the Status table.
    /// </summary>
    public class UnsafeStatus : UnsafeINamedEntity, IUnsafeEntity
    {
        public UnsafeStatus(Name id, string description=null)
            : base(id)
        {
            this.Description = description;
        }

        internal UnsafeStatus(string id, string description)
            : base(new Name(id))
        {
            this.Description = description;
        }

        public override IEntity<Name, string> Clone()
        {
            return new UnsafeStatus(
                this.Id,
                this.Description
            );
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }
    }
}