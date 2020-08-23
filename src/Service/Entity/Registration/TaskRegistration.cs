using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class TaskRegistration
        : IEntityRegistration<Task, Id, int>
    {
        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._repo; } }

        public TaskRegistration(ITaskRepo taskRepo)
            : base(taskRepo)
        { }
    }
}