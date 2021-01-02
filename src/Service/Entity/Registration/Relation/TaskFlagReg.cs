using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="ITaskFlagReg"/>
    public class TaskFlagReg
        : ABCEntityRelationReg
        <
            ITaskFlag,
            ITask,
            Id,
            int,
            IFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >, ITaskFlagReg
    {
        protected ITaskFlagRepo _taskFlagRepo
        { get { return (ITaskFlagRepo) this._repo; } }

        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._leftRepo; } }

        protected readonly ITaskUpdate _taskUpdate;

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public TaskFlagReg(
            ITaskFlagRepo taskFlagRepo,
            ITaskRepo taskRepo,
            ITaskUpdate taskUpdate,
            IFlagRepo flagRepo
        )
            : base(taskFlagRepo, taskRepo, flagRepo)
        {
            this.AssertNotNull(taskUpdate, "taskUpdate");
            this._taskUpdate = taskUpdate;
        }

        public override ITaskFlag Register(ITaskFlag e)
        {
            // NOTE: This code exists in TaskFlagReg.Register(),
            // MetaTaskFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            ITask t;
            IFlag f;
            this._taskFlagRepo.BeginTransaction(true);
            try
            {
                t = this.GetLeftEntity(e);
                f = this.GetRightEntity(e);
            } catch (ArgumentException exc)
            {
                this._taskFlagRepo.DiscardTransaction();
                throw new ArgumentException("Could not retrieve an associated entity.", exc);
            }

            this._taskUpdate.AmendPoints(
                t,
                PointTotal.ApplyPenalty(t.Points, f.Penalty, f.IsFlatPenalty)
            );

            try
            {
                this._taskRepo.Update(t);
                this._taskFlagRepo.Insert(e);
                this._taskFlagRepo.EndTransaction();
                return e;
            }
            catch (ArgumentException exc)
            {
                this._taskRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }
    }
}