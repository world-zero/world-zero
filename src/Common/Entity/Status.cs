using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Status is a entity for a tuple of the Status table.
    /// </summary>
    public class Status : INamedEntity
    {
        public Status(Name id, string description=null)
            : base(id)
        {
            this.Description = description;
        }

        internal Status(string id, string description)
            : base(new Name(id))
        {
            this.Description = description;
        }

        public override IEntity<Name, string> DeepCopy()
        {
            return new Status(
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