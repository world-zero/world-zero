using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;
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

        public string Description { get; set; }
    }
}