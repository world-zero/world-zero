using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Primary
{
    public interface ILocationService
        : IEntityService<ILocation, Id, int>
    { }
}