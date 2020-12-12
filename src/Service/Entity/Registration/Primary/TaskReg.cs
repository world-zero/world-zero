using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class TaskReg
        : ABCEntityReg<UnsafeTask, Id, int>
    {
        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._repo; } }

        public TaskReg(ITaskRepo taskRepo)
            : base(taskRepo)
        { }
    }
}