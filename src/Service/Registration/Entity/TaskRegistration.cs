using WorldZero.Service.Interface.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
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