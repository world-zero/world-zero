using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeNamedEntity
        : IUnsafeEntity<Name, string>, INamedEntity
    {
        public IUnsafeNamedEntity()
            : base(null)
        { }

        public IUnsafeNamedEntity(Name name)
            : base(null)
        {
            this.Id = name;
        }
    }
}