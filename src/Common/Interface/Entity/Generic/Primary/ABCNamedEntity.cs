using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class ABCNamedEntity
        : ABCEntity<Name, string>, INamedEntity
    {
        public ABCNamedEntity(Name name)
            : base(null)
        {
            this.Id = name;
        }
    }
}