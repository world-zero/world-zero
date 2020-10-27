using System;
using WorldZero.Service.Registration.Entity.Relation;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

// Do I want to return modified entities that can't have sensitive data edited?
//      like so no one can edit the status of a praxis w/o a praxis service class

// General Functionality
// TODO: make sure participants are only completing metatasks they have access to
// TODO: after a praxis is moved out of active and into retired, allocate the points
//      have this be a method, but on what Praxis CRUD service class? U?
// TODO: A character cannot submit a praxis for a task that is more than 2 levels greater than their level (level 2 can submit for level 4, but not level 5)
//      Characters need to be able to set a praxis as in-progress, and that the level rule is
//      performed AT THAT TIME ONLY.
// TODO: Make sure AreDueling is reasonable - make sure this check is applied on post-user-adds/-deletes via PraxisParticipantReg.Register()

// Era Magic Numbers
// So they need to live on era, obviously. But how do I want to access them?
//      Static properties of EraReg that gets them from the latest instance?
//
// TODO: A character can only have 20 being-voted-on / in-progress praxises at a time.
//      Have this be static and controlled by era; add the member name to that ticket
// TODO: Normally, characters can only complete a task once (have this be a static field controlled by Era)
//      Have the Reiterator-skill's max submission count live on Era too
// TODO: if a praxis is flagged as inappropriate, remove some of the base points; make this be a number/bool, like MetaTask; add to magic numbers ticket
//      naturally, throw onto era somehow

namespace WorldZero.Service.Registration.Entity
{
    public class PraxisReg
        : IEntityReg<Praxis, Id, int>
    {
        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._repo; } }

        protected readonly ITaskRepo _taskRepo;
        protected readonly IMetaTaskRepo _mtRepo;
        protected readonly IStatusRepo _statusRepo;
        protected readonly PraxisParticipantReg _ppReg;

        public PraxisReg(
            IPraxisRepo praxisRepo,
            ITaskRepo taskRepo,
            IMetaTaskRepo mtRepo,
            IStatusRepo statusRepo,
            PraxisParticipantReg ppReg
        )
            : base(praxisRepo)
        {
            if (taskRepo == null) throw new ArgumentNullException("taskRepo");
            if (mtRepo == null) throw new ArgumentNullException("mtRepo");
            if (statusRepo == null) throw new ArgumentNullException("statusRepo");
            if (ppReg == null) throw new ArgumentNullException("ppReg");
            this._statusRepo = statusRepo;
            this._taskRepo = taskRepo;
            this._mtRepo = mtRepo;
            this._ppReg = ppReg;
        }

        /// <summary>
        /// Register the praxis and supplied participants.
        /// </summary>
        /// <remarks>
        /// A praxis can only be registered if it is Active or In Progress.
        /// <br />
        /// A praxis can only be registered if it has an Active task associated
        /// with it.
        /// <br />
        /// A praxis can only be registered if it has an Active meta task
        /// associated with it, if a meta task is supplied.
        /// </remarks>
        public override Praxis Register(Praxis p)
        {
            this.AssertNotNull(p, "p");
            Task t = this._verifyTask(p);
            MetaTask mt = this._verifyMetaTask(p);

            if (   (p.StatusId != StatusReg.InProgress.Id)
                && (p.StatusId != StatusReg.Active.Id)   )
            {
                throw new ArgumentException("A praxis can only be Active or In Progress");
            }

            return base.Register(p);
        }

        private Task _verifyTask(Praxis p)
        {
            Task t;
            try
            {
                t = this._taskRepo.GetById(p.TaskId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid task ID of {p.TaskId.Get}."); }

            if (t.StatusId != StatusReg.Active.Id)
                throw new ArgumentException("A praxis cannot be submitted for a non-active task.");

            return t;
        }

        private MetaTask _verifyMetaTask(Praxis p)
        {
            MetaTask mt = null;
            try
            {
                if (p.MetaTaskId != null)
                    mt = this._mtRepo.GetById(p.MetaTaskId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid meta task ID of {p.MetaTaskId.Get}."); }

            if (   (mt != null)
                && (mt.StatusId != StatusReg.Active.Id)   )
            {
                throw new ArgumentException("A praxis cannot be submitted for a non-active meta task.");
            }

            return mt;
        }
    }
}