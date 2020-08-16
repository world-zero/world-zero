using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity.RAM
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMNamedEntityRepo<Entity>
        : IRAMEntityRepo<Entity, Name, string>,
          INamedEntityRepo<Entity>
        where Entity : INamedEntity
    { }
}