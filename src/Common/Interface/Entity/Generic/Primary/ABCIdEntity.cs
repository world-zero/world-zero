using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="ABCEntity"/>
    /// <summary>
    /// This class is used for entities that have a `Id` primary key. This
    /// value will be 0 when unspecified as the unset state and if not supplied
    /// to the appropriate constructor. This is done to allow for instantiation
    /// and insertion to a repo at a later time as repos are responsible for
    /// auto-generated IDs.
    /// </summary>
    public abstract class ABCIdEntity : ABCEntity<Id, int>
    {
        public ABCIdEntity()
            : base(new Id(0))
        { }

        public ABCIdEntity(Id id)
            : base(new Id(0))
        {
            this.Id = id;
        }
    }
}