using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="ABCEntity"/>
    /// <summary>
    /// This class is used for entities that have a `Name` primary key. This
    /// can be either null for unset, or set.
    /// </summary>
    public abstract class ABCNamedEntity : ABCEntity<Name, string>
    {
        public ABCNamedEntity()
            : base(null)
        {
        }

        public ABCNamedEntity(Name name)
            : base(null)
        {
            this.Id = name;
        }
    }
}