using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class TaskReg
        : IEntityReg<UnsafeTask, Id, int>
    {
        protected IUnsafeTaskRepo _taskRepo
        { get { return (IUnsafeTaskRepo) this._repo; } }

        public TaskReg(IUnsafeTaskRepo taskRepo)
            : base(taskRepo)
        { }
    }
}