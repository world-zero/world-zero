using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `INamedEntity`.
    /// </summary>
    public interface INamedEntityRepo<NamedEntity>
        : IEntityRepo<NamedEntity, Name, string>
        where NamedEntity : INamedEntity
    { }
}