using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface ILocationRepo
        : IIdEntityRepo<ILocation>
    { }
}