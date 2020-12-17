using System;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="ITaskReg"/>
    public class TaskReg
        : ABCEntityReg<ITask, Id, int>, ITaskReg
    {
        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._repo; } }

        protected readonly IFactionRepo _factionRepo;

        public TaskReg(ITaskRepo taskRepo, IFactionRepo factionRepo)
            : base(taskRepo)
        {
            this.AssertNotNull(factionRepo, "factionRepo");
            this._factionRepo = factionRepo;
        }

        public override ITask Register(ITask t)
        {
            this.AssertNotNull(t, "t");
            this.BeginTransaction(true);
            try
            {
                if (t.FactionId != null)
                    this._factionRepo.GetById(t.FactionId);
            }
            catch (ArgumentException)
            {
                this.DiscardTransaction();
                throw new ArgumentException($"Task of ID {t.Id.Get} has an invalid Faction ID of {t.FactionId.Get}.");
            }
            var r = base.Register(t);
            this.EndTransaction();
            return r;
        }
    }
}