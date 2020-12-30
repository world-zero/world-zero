using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Relation
{
    public interface ITaskFlagService
        : IEntityService<ITaskFlag, Id, int>
    { }
}