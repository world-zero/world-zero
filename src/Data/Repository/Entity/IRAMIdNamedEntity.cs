using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Data.Repository.Entity
{
    // TODO: Implement the name uniqueness enforcement and test.

    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMIdNamedEntityRepo<Entity>
        : IRAMIdEntityRepo<Entity>,
          IIdNamedEntityRepo<Entity>
        where Entity : IIdNamedEntity
    {
        public IRAMIdNamedEntityRepo()
            : base()
        { }
    }
}