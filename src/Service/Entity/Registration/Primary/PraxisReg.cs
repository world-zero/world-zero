using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Service.Entity.Registration.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IPraxisReg"/>
    public class PraxisReg
        : ABCEntityReg<IPraxis, Id, int>, IPraxisReg
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
        public override IPraxis Register(IPraxis p)
        {
            throw new ArgumentException("You must supply participant(s).");
        }

        public async Task<IPraxis> RegisterAsync(IPraxis p, IPraxisParticipant pp)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(pp, "pp");
            this._verifyStatus(p);
            return await Task.Run(() => this.Register(p, pp));
        }

        public async
        Task<IPraxis> RegisterAsync(IPraxis p, List<IPraxisParticipant> pps)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(pps, "pps");
            this._verifyStatus(p);
            return await Task.Run(() => this.Register(p, pps));
        }

        public IPraxis Register(IPraxis p, IPraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            var pps = new List<IPraxisParticipant>();
            pps.Add(pp);
            return this.Register(p, pps);
        }

        public IPraxis Register(IPraxis p, List<IPraxisParticipant> pps)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(pps, "pps");
            this._verifyStatus(p);
            if (pps.Count == 0) 
                throw new ArgumentException("pps contains no praxis participants.");
            if ( (p.AreDueling) && (pps.Count != 2)  )
                throw new ArgumentException("The praxis thinks the participants are dueling but there are not two participants exactly.");

            foreach (IPraxisParticipant pp in pps)
            {
                if (pp.PraxisId != null)
                    throw new ArgumentException("The praxis participant is already associated with a praxis, which cannot be the supplied praxis since it has an unset ID.");
            }
            this._praxisRepo.BeginTransaction(true);
            try
            {
                ITask t = this._verifyTask(p);
                IMetaTask mt = this._verifyMetaTask(p);
                this._praxisRepo.Insert(p);
                this._praxisRepo.Save();
                foreach (UnsafePraxisParticipant pp in pps)
                {
                    // This cannot use the IPPUpdate since we do not want to be
                    // able to edit relations.
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

        private void _verifyStatus(IPraxis p)
        {
            if (   (p.StatusId != ConstantStatuses.InProgress.Id)
                && (p.StatusId != ConstantStatuses.Active.Id)   )
            {
                throw new ArgumentException("A praxis can only be Active or In Progress");
            }
        }

        private ITask _verifyTask(IPraxis p)
        {
            ITask t;
            try
            {
                t = this._taskRepo.GetById(p.TaskId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid task ID of {p.TaskId.Get}."); }

            if (t.StatusId != ConstantStatuses.Active.Id)
                throw new ArgumentException("A praxis cannot be submitted for a non-active task.");

            return t;
        }

        /// <remarks>
        /// A `MetaTask` is not required for a `Praxis`, so this can return
        /// `null`.
        /// </remarks>
        private IMetaTask _verifyMetaTask(IPraxis p)
        {
            IMetaTask mt = null;
            try
            {
                if (p.MetaTaskId != null)
                    mt = this._mtRepo.GetById(p.MetaTaskId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid meta task ID of {p.MetaTaskId.Get}."); }

            if (   (mt != null)
                && (mt.StatusId != ConstantStatuses.Active.Id)   )
            {
                throw new ArgumentException("A praxis cannot be submitted for a non-active meta task.");
            }

            return mt;
        }
    }
}