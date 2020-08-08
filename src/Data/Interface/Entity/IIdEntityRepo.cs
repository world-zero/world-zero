using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Entity
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdEntity`.
    /// </summary>
    public interface IIdEntityRepo<IdEntity>
        : IEntityRepo<IdEntity, Id, int>
        where IdEntity : IIdEntity
    { }
}