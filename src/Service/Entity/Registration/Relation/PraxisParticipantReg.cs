using System;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Service.Entity.Registration.Primary;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IPraxisParticipantReg"/>
    public class PraxisParticipantReg
        : ABCEntityRelationReg
        <
            IPraxisParticipant,
            IPraxis,
            Id,
            int,
            ICharacter,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >, IPraxisParticipantReg
    {
        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._rightRepo; } }

        protected readonly IMetaTaskRepo _mtRepo;
        protected readonly ITaskRepo _taskRepo;
        protected readonly IFactionRepo _factionRepo;
        protected readonly EraReg _eraReg;
        protected ISet<Name> _praxisLiveStatuses;

        public PraxisParticipantReg(
            IPraxisParticipantRepo praxisParticipantRepo,
            IPraxisRepo praxisRepo,
            ICharacterRepo characterRepo,
            IMetaTaskRepo mtRepo,
            ITaskRepo taskRepo,
            IFactionRepo factionRepo,
            EraReg eraReg
        )
            : base(praxisParticipantRepo, praxisRepo, characterRepo)
        {
            this.AssertNotNull(mtRepo, "mtRepo");
            this.AssertNotNull(taskRepo, "taskRepo");
            this.AssertNotNull(factionRepo, "factionRepo");
            this.AssertNotNull(eraReg, "eraReg");
            this._mtRepo = mtRepo;
            this._taskRepo = taskRepo;
            this._factionRepo = factionRepo;
            this._eraReg = eraReg;

            this._praxisLiveStatuses = new HashSet<Name>();
            this._praxisLiveStatuses.Add(ConstantStatuses.InProgress.Id);
            this._praxisLiveStatuses.Add(ConstantStatuses.Active.Id);
        }

        public override IPraxisParticipant Register(IPraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            this._ppRepo.BeginTransaction(true);
            IPraxis p      = this._verifyPraxis(pp);
            ICharacter c   = this._verifyCharacter(pp);
            IEra activeEra = this._eraReg.GetActiveEra();
                            this._verifyLevel(c, activeEra, p);
                            this._verifyPraxisCount(c, activeEra);
            int count     = this._verifyPPCount(pp, c, activeEra);
            IMetaTask mt   = this._getMetaTask(p);
                            this._verifyFactionsMatch(c, mt);
                            this._verifyDuel(p);
            var r         = base.Register(pp);
            this.EndTransaction();
            return r;
        }

        private IPraxis _verifyPraxis(IPraxisParticipant pp)
        {
            IPraxis p;
            try
            {
                p = this._praxisRepo.GetById(pp.PraxisId);
            }
            catch (ArgumentException)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"PraxisParticipant of ID {pp.Id.Get} has an invalid praxis ID of {pp.PraxisId.Get}.");
            }

            if (   (p.StatusId != ConstantStatuses.InProgress.Id)
                && (p.StatusId != ConstantStatuses.Active.Id)      )
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("A participant can only be registered for an Active or In Progress task.");
            }

            return p;
        }

        private ICharacter _verifyCharacter(IPraxisParticipant pp)
        {
            ICharacter c;
            try
            {
                c = this._characterRepo.GetById(pp.CharacterId);
                return c;
            }
            catch (ArgumentException)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"PraxisParticipant of ID {pp.Id.Get} has an invalid character ID of {pp.CharacterId.Get}.");
            }
        }

        private void _verifyLevel(ICharacter c, IEra activeEra, IPraxis p)
        {
            int bufferedLevel = c.EraLevel.Get + activeEra.TaskLevelBuffer.Get;
            int reqLevel;
            try
            {
                reqLevel = this._taskRepo.GetById(p.TaskId).Level.Get;
            }
            catch (ArgumentException)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("The Task the participant is participating on does not exist.");
            }

            if (bufferedLevel < reqLevel)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"Participant {c.Name.Get} ({c.Id.Get}) has level {c.EraLevel.Get}, when combined with buffer {bufferedLevel}, does not reach level requirement ({reqLevel}).");
            }
        }

        private void _verifyPraxisCount(ICharacter c, IEra activeEra)
        {
            int praxisCount = this._ppRepo
                .GetPraxisCount(c.Id, this._praxisLiveStatuses);
            praxisCount++;
            if (praxisCount > activeEra.MaxPraxises)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character {c.Name.Get} ({c.Id.Get}) has already reached their maximum amount of allowed live praxises, {praxisCount} (max of {activeEra.MaxPraxises}).");
            }
        }

        private int _verifyPPCount(
            IPraxisParticipant pp,
            ICharacter c,
            IEra activeEra
        )
        {
            int nextCount =
                this._ppRepo.GetCharacterSubmissionCountViaPraxisId(
                    pp.PraxisId,
                    pp.CharacterId
                );
            if (++nextCount <= activeEra.MaxTaskCompletion)
                return nextCount;

            // The reiterator ability allows for more task recompletions, see
            // if they are in a faction with that ability.

            if (c.FactionId == null)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTaskCompletion} time(s).");
            }

            IFaction f;
            try
            {
                f = this._factionRepo.GetById(c.FactionId);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("A character has a faction that does not exist, this should not be possible; only register entities via their registration class and update them via their updating class.", e); }

            if (   (f.AbilityId == null )
                || (f.AbilityId != ConstantAbilities.Reiterator.Id))
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTaskCompletion} time(s).");
            }

            if (nextCount > activeEra.MaxTaskCompletionReiterator)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTaskCompletion} time(s).");
            }

            return nextCount;
        }

        /// <remarks>
        /// A `MetaTask` is not required for a `Praxis`, so this can return
        /// `null`.
        /// </remarks>
        private IMetaTask _getMetaTask(IPraxis p)
        {
            if (p.MetaTaskId == null)
                return null;
            try
            {
                return this._mtRepo.GetById(p.MetaTaskId);
            }
            catch (ArgumentException)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid meta task ID of {p.MetaTaskId.Get}.");
            }
        }

        private void _verifyFactionsMatch(ICharacter c, IMetaTask mt)
        {
            if (mt == null)
                return;

            if (c.FactionId == null)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("The meta task has a sponsoring faction but the participant is unaligned.");
            }

            if (mt.StatusId != ConstantStatuses.Active.Id)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("A participant cannot be submitted for a non-active meta task.");
            }

            if (mt.FactionId != c.FactionId)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("The meta task is not sponsored by the character's faction.");
            }
        }

        /// <remarks>
        /// If there's no participants, then we know this is being
        /// called in tandem with PraxisReg.Register(), since that will
        /// not let a Praxis be registered without a participant, and
        /// that a praxis should never exist without any participants
        /// outside of that time. We can savely register this pp because
        /// PraxisReg would not allow a dueling praxis to be registered
        /// if it had the incorrect number of participants.
        /// </remarks>
        private void _verifyDuel(IPraxis p)
        {
            if (p.AreDueling)
            {
                int ppCount =
                    this._ppRepo.GetParticipantCountViaPraxisId(p.Id);
                if (ppCount >= 2)
                {
                    this._ppRepo.DiscardTransaction();
                    throw new ArgumentException($"The associated praxis is set to dueling, which only allows for 2 participants, not {ppCount}.");
                }
            }
        }
    }
}