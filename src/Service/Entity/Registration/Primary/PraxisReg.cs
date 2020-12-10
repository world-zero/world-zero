using System;
using System.Collections.Generic;
using WorldZero.Service.Entity.Registration.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    /// <summary>
    /// Register the praxis and supplied participants.
    /// </summary>
    /// <remarks>
    /// When furthering development, be mindful about how Praxis needs a
    /// participant - both PraxisReg and PraxisParticipantReg really
    /// rely on this fact.
    /// <br />
    /// This class will ensure that a duel is between two characters.
    /// <br />
    /// A praxis can only be registered if it is Active or In Progress.
    /// <br />
    /// A praxis can only be registered if it has an Active task associated
    /// with it.
    /// <br />
    /// A praxis can only be registered if it has an Active meta task
    /// associated with it, if a meta task is supplied.
    /// <br />
    /// A praxis participant can only be registered if it's `PraxisId` is null.
    /// <br />
    /// When furthering development, be mindful about how PraxisReg needs a
    /// participant - both PraxisReg and PraxisParticipantReg rely on this
    /// fact.
    /// </remarks>
    public class PraxisReg
        : IEntityReg<UnsafePraxis, Id, int>
    {
        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._repo; } }

        protected readonly IUnsafeTaskRepo _taskRepo;
        protected readonly IUnsafeMetaTaskRepo _mtRepo;
        protected readonly IUnsafeStatusRepo _statusRepo;
        protected readonly PraxisParticipantReg _ppReg;

        public PraxisReg(
            IUnsafePraxisRepo praxisRepo,
            IUnsafeTaskRepo taskRepo,
            IUnsafeMetaTaskRepo mtRepo,
            IUnsafeStatusRepo statusRepo,
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
        public override UnsafePraxis Register(UnsafePraxis p)
        {
            throw new ArgumentException("You must supply participant(s).");
        }

        public UnsafePraxis Register(UnsafePraxis p, UnsafePraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            var pps = new List<UnsafePraxisParticipant>();
            pps.Add(pp);
            return this.Register(p, pps);
        }

        public UnsafePraxis Register(UnsafePraxis p, List<UnsafePraxisParticipant> pps)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(pps, "pps");
            this._verifyStatus(p);
            if (pps.Count == 0) 
                throw new ArgumentException("pps contains no praxis participants.");
            if ( (p.AreDueling) && (pps.Count != 2)  )
                throw new ArgumentException("The praxis thinks the participants are dueling but there are not two participants exactly.");

            foreach (UnsafePraxisParticipant pp in pps)
            {
                if (pp.PraxisId != null)
                    throw new ArgumentException("The praxis participant is already associated with a praxis, which cannot be the supplied praxis since it has an unset ID.");
            }
            this._praxisRepo.BeginTransaction(true);
            try
            {
                UnsafeTask t = this._verifyTask(p);
                UnsafeMetaTask mt = this._verifyMetaTask(p);
                this._praxisRepo.Insert(p);
                this._praxisRepo.Save();
                foreach (UnsafePraxisParticipant pp in pps)
                {
                    pp.PraxisId = p.Id;
                    this._ppReg.Register(pp);
                }
                this._praxisRepo.EndTransaction();
                return p;
            }
            catch (ArgumentException e)
            {
                this._praxisRepo.DiscardTransaction();
                throw new ArgumentException("An error occurred during praxis registration, discarding praxis / participant(s) registration.", e);
            }
        }

        private void _verifyStatus(UnsafePraxis p)
        {
            if (   (p.StatusId != StatusReg.InProgress.Id)
                && (p.StatusId != StatusReg.Active.Id)   )
            {
                throw new ArgumentException("A praxis can only be Active or In Progress");
            }
        }

        private UnsafeTask _verifyTask(UnsafePraxis p)
        {
            UnsafeTask t;
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
        private UnsafeMetaTask _verifyMetaTask(UnsafePraxis p)
        {
            UnsafeMetaTask mt = null;
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