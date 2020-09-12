using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ITaskFlagRepo">
    public class RAMTaskFlagRepo
        : IRAMIdNameRepo<TaskFlag>,
          ITaskFlagRepo
    { }
}