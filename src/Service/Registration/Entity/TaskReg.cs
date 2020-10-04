using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class TaskReg
        : IEntityReg<Task, Id, int>
    {
        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._repo; } }

        public TaskReg(ITaskRepo taskRepo)
            : base(taskRepo)
        { }
    }
}