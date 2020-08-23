using System;
using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class PraxisRegistration
        : IEntityRegistration<Praxis, Id, int>
    {
        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._repo; } }

        protected readonly ITaskRepo _taskRepo;
        protected readonly IStatusRepo _statusRepo;

        public PraxisRegistration(
            IPraxisRepo praxisRepo,
            ITaskRepo taskRepo,
            IStatusRepo statusRepo
        )
            : base(praxisRepo)
        {
            if ( (statusRepo == null) || (taskRepo == null) )
                throw new ArgumentException(NullRepoException);
            this._taskRepo = taskRepo;
            this._statusRepo = statusRepo;
        }

        /// <summary>
        /// Create the praxis and save it. This will ensure that the
        /// praxis has a valid task ID and status ID.
        /// </summary>
        public override Praxis Register(Praxis p)
        {
            this.AssertNotNull(p);
            try
            {
                this._statusRepo.GetById(p.StatusId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Praxis of ID {p.Id} has an invalid status ID of {p.StatusId}.");
            }
            try
            {
                if (p.TaskId != null)
                    this._taskRepo.GetById(p.TaskId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Praxis of ID {p.Id} has an invalid task ID of {p.TaskId}.");
            }
            return base.Register(p);
        }
    }
}