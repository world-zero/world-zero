using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public class Tag : INamedEntity
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public Tag(Name name)
            : base(name)
        { }

        internal Tag(string name, string description)
            : base(new Name(name))
        {
            this.Description = description;
        }

        public override IEntity<Name, string> Clone()
        {
            var t = new Tag(this.Id);
            t.Description = this.Description;
            return t;
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }
    }
}
