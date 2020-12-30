using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Relation
{
    /// <inheritdoc cref="ITaskFlagUpdate"/>
    public class TaskFlagUpdate
        : ABCEntityUpdate<ITaskFlag, Id, int>,
        ITaskFlagUpdate
    {
        public TaskFlagUpdate(ITaskFlagRepo tfRepo)
            : base(tfRepo)
        { }
    }
}