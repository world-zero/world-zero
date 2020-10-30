using System;
using System.Collections.Generic;
using WorldZero.Service.Registration.Entity.Relation;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;

// Do I want to return modified entities that can't have sensitive data edited?
//      like so no one can edit the status of a praxis w/o a praxis service class

// Whichever function controls changing a character's faction should consider
// how to handle in progress / active meta tasks they've signed up for.

// General Functionality
// TODO: after a praxis is moved out of active and into retired, allocate the points
//      have this be a method, but on what Praxis CRUD service class? U?
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
// TODO: A character cannot submit a praxis for a task that is more than 2 levels greater than their level (level 2 can submit for level 4, but not level 5)
//      Characters need to be able to set a praxis as in-progress, and that the level rule is
//      performed AT THAT TIME ONLY.
//      This should be done on praxis participant. Naturally, we want to have this level difference exist on an era
//      I have included the addition to PraxisParticipantReg's remarks below.
    /// <br />
    /// The character's level versus the task's level is computed here, as they
    /// register with / on a praxis. This will allow someone to register as in
    /// progress for a praxis and still be able to complete it after an era
    /// rolls over.

namespace WorldZero.Service.Registration.Entity
{
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
    /// <br />
    /// A praxis participant can only be registered if it's `PraxisId` is null.
    /// </remarks>
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
        /// Do not use this method, it will only throw an argument exception.
        /// Instead, use `PraxisReg.Register(Praxis, List<PraxisParticipant>)`
        /// or `PraxisReg.Register(Praxis, PraxisParticipant)`.
        /// </summary>
        public override Praxis Register(Praxis p)
        {
            throw new ArgumentException("You must supply participant(s).");
        }

        public Praxis Register(Praxis p, PraxisParticipant pp)
        {
            var pps = new List<PraxisParticipant>();
            pps.Add(pp);
            return this.Register(p, pps);
        }

        public Praxis Register(Praxis p, List<PraxisParticipant> pps)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(pps, "pps");
            if (pps.Count == 0) 
                throw new ArgumentException("pps contains no praxis participants.");
            this._verifyStatus(p);

            foreach (PraxisParticipant pp in pps)
            {
                if (pp.PraxisId != null)
                    throw new ArgumentException("The praxis participant is already associated with a praxis, which cannot be the supplied praxis since it has an unset ID.");
            }
            this._praxisRepo.BeginTransaction();
            try
            {
                Task t = this._verifyTask(p);
                MetaTask mt = this._verifyMetaTask(p);
                this._praxisRepo.Insert(p);
                this._praxisRepo.Save();
                foreach (PraxisParticipant pp in pps)
                {
                    pp.PraxisId = p.Id;
                    this._ppReg.Register(pp);
                }
            }
            catch (ArgumentException e)
            {
                this._praxisRepo.DiscardTransaction();
                throw new ArgumentException("An error occurred during praxis registration, discarding praxis / participant(s) registration.", e);
            }
            this._praxisRepo.EndTransaction();

            return p;
        }

        private void _verifyStatus(Praxis p)
        {
            if (   (p.StatusId != StatusReg.InProgress.Id)
                && (p.StatusId != StatusReg.Active.Id)   )
            {
                throw new ArgumentException("A praxis can only be Active or In Progress");
            }
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

        /// <remarks>
        /// A `MetaTask` is not required for a `Praxis`, so this can return
        /// `null`.
        /// </remarks>
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