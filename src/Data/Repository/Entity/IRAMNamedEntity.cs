using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Data.Repository.Entity
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMNamedEntityRepo<Entity>
        : IRAMEntityRepo<Entity, Name, string>,
          INamedEntityRepo<Entity>
        where Entity : INamedEntity
    { }
}