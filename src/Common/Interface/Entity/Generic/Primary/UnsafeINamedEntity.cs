using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class UnsafeINamedEntity
        : UnsafeIEntity<Name, string>, INamedEntity
    {
        public UnsafeINamedEntity()
            : base(null)
        {
        }

        public UnsafeINamedEntity(Name name)
            : base(null)
        {
            this.Id = name;
        }
    }
}