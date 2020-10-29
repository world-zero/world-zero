using System;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    /// <remarks>
    /// This will not ensure that one Player is having several of their
    /// characters participating on the same praxis.
    /// <br />
    /// A Praxis should always have at least one participant.
    /// </remarks>
    public class PraxisParticipantReg
        : IEntityRelationReg
        <
            PraxisParticipant,
            Praxis,
            Id,
            int,
            Character,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
        >
    {
        protected IPraxisParticipantRepo _praxisParticipantRepo
        { get { return (IPraxisParticipantRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._rightRepo; } }

        protected readonly IMetaTaskRepo _mtRepo;

        public PraxisParticipantReg(
            IPraxisParticipantRepo praxisParticipantRepo,
            IPraxisRepo praxisRepo,
            ICharacterRepo characterRepo,
            IMetaTaskRepo mtRepo
        )
            : base(praxisParticipantRepo, praxisRepo, characterRepo)
        {
            this.AssertNotNull(mtRepo, "mtRepo");
            this._mtRepo = mtRepo;
        }

        public override PraxisParticipant Register(PraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            Praxis p = this._verifyPraxis(pp);
            Character c = this._verifyCharacter(pp);
            MetaTask mt = this._getMetaTask(p);
            this._verifyFaction(c, mt);
            return base.Register(pp);
        }

        private Praxis _verifyPraxis(PraxisParticipant pp)
        {
            Praxis p;
            try
            {
                p = this._praxisRepo.GetById(pp.PraxisId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"PraxisParticipant of ID {pp.Id.Get} has an invalid praxis ID of {pp.PraxisId.Get}."); }

            if (   (p.StatusId != StatusReg.InProgress.Id)
                && (p.StatusId != StatusReg.Active.Id)      )
            {
                throw new ArgumentException("A participant can only be registered for an active or in-progres task.");
            }

            return p;
        }

        private Character _verifyCharacter(PraxisParticipant pp)
        {
            Character c;
            try
            {
                return c = this._characterRepo.GetById(pp.CharacterId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"CharacterParticipant of ID {pp.Id.Get} has an invalid character ID of {pp.CharacterId.Get}."); }
        }

        private void _verifyFaction(Character c, MetaTask mt)
        {
            if (mt == null)
                return;

            if (c.FactionId == null)
                throw new ArgumentException("The meta task has a sponsoring faction but the character is unaligned.");

            if (mt.FactionId != c.FactionId)
                throw new ArgumentException("The meta task is not sponsored by the character's faction.");
        }

        /// <remarks>
        /// A `MetaTask` is not required for a `Praxis`, so this can return
        /// `null`.
        /// </remarks>
        private MetaTask _getMetaTask(Praxis p)
        {
            if (p.MetaTaskId == null)
                return null;
            MetaTask mt;
            try
            {
                mt = this._mtRepo.GetById(p.MetaTaskId);
            }
            catch (ArgumentException)
            { throw new ArgumentException($"Praxis of ID {p.Id.Get} has an invalid meta task ID of {p.MetaTaskId.Get}."); }
            return mt;
        }

    }
}