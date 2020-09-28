using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IEntity"/>
    /// <summary>
    /// This class is used for entities that have a `Id` primary key. This
    /// value will be 0 when unspecified as the unset state and if not supplied
    /// to the appropriate constructor. This is done to allow for instantiation
    /// and insertion to a repo at a later time as repos are responsible for
    /// auto-generated IDs.
    /// </summary>
    public abstract class IIdEntity : IEntity<Id, int>
    {
        public IIdEntity()
            : base(new Id(0))
        { }

        public IIdEntity(Id id)
            : base(new Id(0))
        {
            this.Id = id;
        }
    }
}