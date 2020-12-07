using System;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Service.Entity.Registration.Primary;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    /// <remarks>
    /// A Praxis should always have at least one participant.
    /// <br />
    /// This class will ensure that a duel is between two characters.
    /// <br />
    /// This will ensure that one Player is not having several of their
    /// characters participating on the same praxis.
    /// <br />
    /// This will set `PraxisParticipant.Count` on registration and validate it
    /// against the active era's `MaxTasks` or `MaxTasksReiterator`, as
    /// appropriate. If this is used in part of a series of registrations, this
    /// will not revert a `PraxisParticipant`'s Count back to the
    /// pre-registration value, it will be an artifact.
    /// <br />
    /// The character's level versus the task's level is computed here, as they
    /// register with / on a praxis. This will allow someone to register as In
    /// Progress for a praxis and still be able to complete it after an Era
    /// roll-over. For example, if someone's EraLevel is X, and
    /// `Era.TaskLevelBuffer` is Y, then someone can be a participant of tasks
    /// of X+Y and below.
    /// <br />
    /// This will ensure that a character does not have more than the allowed
    /// number of in progress / active praxises, as defined by the Era returned
    /// by `EraReg.GetActiveEra()`. That said, if someone has X in progress
    /// praxises and the new Era has the MaxPraxises of X-3, then they will
    /// keep their in progress praxises despite being over the limit.
    /// <br />
    /// When furthering development, be mindful about how PraxisReg needs a
    /// participant - both PraxisReg and PraxisParticipantReg rely on this
    /// fact.
    /// </remarks>
    public class PraxisParticipantReg
        : IEntityRelationReg
        <
            PraxisParticipant,
            Praxis,
            Id,
            int,
            UnsafeCharacter,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected IUnsafeCharacterRepo _characterRepo
        { get { return (IUnsafeCharacterRepo) this._rightRepo; } }

        protected readonly IUnsafeMetaTaskRepo _mtRepo;
        protected readonly ITaskRepo _taskRepo;
        protected readonly IUnsafeFactionRepo _factionRepo;
        protected readonly EraReg _eraReg;
        protected ISet<Name> _praxisLiveStatuses;

        public PraxisParticipantReg(
            IPraxisParticipantRepo praxisParticipantRepo,
            IPraxisRepo praxisRepo,
            IUnsafeCharacterRepo characterRepo,
            IUnsafeMetaTaskRepo mtRepo,
            ITaskRepo taskRepo,
            IUnsafeFactionRepo factionRepo,
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
            this._praxisLiveStatuses.Add(StatusReg.InProgress.Id);
            this._praxisLiveStatuses.Add(StatusReg.Active.Id);
        }

        public override PraxisParticipant Register(PraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            this._ppRepo.BeginTransaction(true);
            Praxis p      = this._verifyPraxis(pp);
            UnsafeCharacter c   = this._verifyCharacter(pp);
            UnsafeEra activeEra = this._eraReg.GetActiveEra();
                            this._verifyLevel(c, activeEra, p);
                            this._verifyPraxisCount(c, activeEra);
            int count     = this._verifyPPCount(pp, c, activeEra);
            UnsafeMetaTask mt   = this._getMetaTask(p);
                            this._verifyFactionsMatch(c, mt);
                            this._verifyDuel(p);
            var r         = base.Register(pp);
            this.EndTransaction();
            return r;
        }

        private Praxis _verifyPraxis(PraxisParticipant pp)
        {
            Praxis p;
            try
            {
                p = this._praxisRepo.GetById(pp.PraxisId);
            }
            catch (ArgumentException)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"PraxisParticipant of ID {pp.Id.Get} has an invalid praxis ID of {pp.PraxisId.Get}.");
            }

            if (   (p.StatusId != StatusReg.InProgress.Id)
                && (p.StatusId != StatusReg.Active.Id)      )
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("A participant can only be registered for an Active or In Progress task.");
            }

            return p;
        }

        private UnsafeCharacter _verifyCharacter(PraxisParticipant pp)
        {
            UnsafeCharacter c;
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

        private void _verifyLevel(UnsafeCharacter c, UnsafeEra activeEra, Praxis p)
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

        private void _verifyPraxisCount(UnsafeCharacter c, UnsafeEra activeEra)
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
            PraxisParticipant pp,
            UnsafeCharacter c,
            UnsafeEra activeEra
        )
        {
            int nextCount =
                this._ppRepo.GetCharacterSubmissionCountViaPraxisId(
                    pp.PraxisId,
                    pp.CharacterId
                );
            if (++nextCount <= activeEra.MaxTasks)
                return nextCount;

            // The reiterator ability allows for more task recompletions, see
            // if they are in a faction with that ability.

            if (c.FactionId == null)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTasks} time(s).");
            }

            UnsafeFaction f;
            try
            {
                f = this._factionRepo.GetById(c.FactionId);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("A character has a faction that does not exist, this should not be possible; only register entities via their registration class and update them via their updating class.", e); }

            if (   (f.AbilityId == null )
                || (f.AbilityId != AbilityReg.Reiterator.Id))
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTasks} time(s).");
            }

            if (nextCount > activeEra.MaxTasksReiterator)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException($"The character can only submit praxises for a task {activeEra.MaxTasks} time(s).");
            }

            return nextCount;
        }

        /// <remarks>
        /// A `MetaTask` is not required for a `Praxis`, so this can return
        /// `null`.
        /// </remarks>
        private UnsafeMetaTask _getMetaTask(Praxis p)
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

        private void _verifyFactionsMatch(UnsafeCharacter c, UnsafeMetaTask mt)
        {
            if (mt == null)
                return;

            if (c.FactionId == null)
            {
                this._ppRepo.DiscardTransaction();
                throw new ArgumentException("The meta task has a sponsoring faction but the participant is unaligned.");
            }

            if (mt.StatusId != StatusReg.Active.Id)
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
        private void _verifyDuel(Praxis p)
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