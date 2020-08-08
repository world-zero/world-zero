using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Entity
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `INamedEntity`.
    /// </summary>
    public interface IIdNamedEntityRepo<IdNamedEntity>
        : IIdEntityRepo<IdNamedEntity>
        where IdNamedEntity : IIdEntity
    { }
}