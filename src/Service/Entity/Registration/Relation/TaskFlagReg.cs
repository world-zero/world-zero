using System;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class TaskFlagReg
        : IEntityRelationReg
        <
            UnsafeTaskFlag,
            UnsafeTask,
            Id,
            int,
            UnsafeFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected ITaskFlagRepo _taskFlagRepo
        { get { return (ITaskFlagRepo) this._repo; } }

        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public TaskFlagReg(
            ITaskFlagRepo taskFlagRepo,
            ITaskRepo taskRepo,
            IFlagRepo flagRepo
        )
            : base(taskFlagRepo, taskRepo, flagRepo)
        { }

        public override UnsafeTaskFlag Register(UnsafeTaskFlag e)
        {
            // NOTE: This code exists in TaskFlagReg.Register(),
            // MetaTaskFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            UnsafeTask t;
            UnsafeFlag f;
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

            t.Points = PointTotal
                .ApplyPenalty(t.Points, f.Penalty, f.IsFlatPenalty);

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