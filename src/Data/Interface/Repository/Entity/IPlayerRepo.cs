using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IIdNamedEntityRepo"/>
    public interface IPlayerRepo
        : IIdNamedEntityRepo<Player>
    { }
}