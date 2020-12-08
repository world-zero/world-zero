using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public class UnsafeTag : UnsafeINamedEntity, IUnsafeEntity
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public UnsafeTag(Name name)
            : base(name)
        { }

        internal UnsafeTag(string name, string description)
            : base(new Name(name))
        {
            this.Description = description;
        }

        public override IEntity<Name, string> Clone()
        {
            var t = new UnsafeTag(this.Id);
            t.Description = this.Description;
            return t;
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }
    }
}
