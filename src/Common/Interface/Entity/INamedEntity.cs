using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IEntity">
    /// <summary>
    /// This class is used for entities that have a `Name` primary key. This
    /// can be either null for unset, or set.
    /// </summary>
    public abstract class INamedEntity : IEntity<Name, string>
    {
        public INamedEntity()
            : base(null)
        {
        }

        public INamedEntity(Name name)
            : base(null)
        {
            this.Id = name;
        }
    }
}