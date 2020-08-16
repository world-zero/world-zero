using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public class Flag : INamedEntity
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public Flag(Name name)
            : base(name)
        { }

        internal Flag(string name, string description)
            : base(new Name(name))
        {
            this.Description = description;
        }

        public override IEntity<Name, string> DeepCopy()
        { 
            return new Flag(
                this.Id
            );
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }
    }
}
