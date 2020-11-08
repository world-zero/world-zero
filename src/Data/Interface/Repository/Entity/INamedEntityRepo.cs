using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `INamedEntity`.
    /// </summary>
    public interface INamedEntityRepo<TNamedEntity>
        : IEntityRepo<TNamedEntity, Name, string>
        where TNamedEntity : INamedEntity
    { }
}