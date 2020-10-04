using System;
using WorldZero.Service.Interface.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
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
            if (taskRepo == null) throw new ArgumentNullException("taskRepo");
            if (statusRepo == null) throw new ArgumentNullException("statusRepo");
            this._taskRepo = taskRepo;
            this._statusRepo = statusRepo;
        }

        /// <summary>
        /// Create the praxis and save it. This will ensure that the
        /// praxis has a valid task ID and status ID.
        /// </summary>
        public override Praxis Register(Praxis p)
        {
            this.AssertNotNull(p, "p");
            Task t;
            try
            {
                t = this._taskRepo.GetById(p.TaskId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Praxis of ID {p.Id} has an invalid task ID of {p.TaskId}.");
            }

            try
            {
                this._statusRepo.GetById(p.StatusId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Praxis of ID {p.Id} has an invalid status ID of {p.StatusId}.");
            }

            if (t.StatusId != new Name("Active"))
                throw new ArgumentException("A praxis cannot be submitted for a non-active task.");

            if ( (p.StatusId != new Name("In Progress"))
              && (p.StatusId != new Name("Active")) )
            {
                throw new ArgumentException("A praxis can only be Active or In Progress");
            }
            return base.Register(p);
        }
    }
}