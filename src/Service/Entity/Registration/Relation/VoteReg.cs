using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Update.Primary;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IVoteReg"/>
    public class VoteReg
        : ABCEntityRelationReg
        <
            IVote,
            ICharacter,
            Id,
            int,
            IPraxisParticipant,
            Id,
            int,
            NoIdRelationDTO<Id, int, Id, int>
        >, IVoteReg
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._leftRepo; } }

        protected readonly ICharacterUpdate _characterUpdate;

        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._rightRepo; } }

        protected readonly IPraxisRepo _praxisRepo;

        public VoteReg(
            IVoteRepo voteRepo,
            ICharacterRepo characterRepo,
            ICharacterUpdate characterUpdate,
            IPraxisParticipantRepo praxisParticipantRepo,
            IPraxisRepo praxisRepo
        )
            : base(voteRepo, characterRepo, praxisParticipantRepo)
        {
            this.AssertNotNull(characterUpdate, "characterUpdate");
            this.AssertNotNull(praxisRepo, "praxisRepo");
            this._characterUpdate = characterUpdate;
            this._praxisRepo = praxisRepo;
        }

        public override IVote Register(IVote v)
        {
            // This does not use base.PreRegisterChecks() as querying the two
            // repositories twice each would be extremely costly for a DB repo.
            this.AssertNotNull(v, "v");
            this._voteRepo.BeginTransaction(true);
            var votingChar     = this._regGetCharacter(v);
            var pp             = this._regGetPP(v);
            var praxis         = this._regGetPraxis(pp);
            var recChar        = this._regGetRecChar(pp);
            var votersCharsIds = this._regGetVotersCharsIds(votingChar);
            var praxisCharsIds = this._regGetPraxisCharsIds(praxis);

            // Make sure someone isn't voting on their own praxis.
            if (votersCharsIds.Intersect(praxisCharsIds).Count() != 0)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("A player is attempting to vote on their own praxis.");
            }

            // Make sure a Player isn't voting on a praxis multiple times.
            var votesCharIds = this._regGetPraxisVoters(pp);
            if (   (votesCharIds != null)
                && (votesCharIds.Intersect(votersCharsIds).Count() != 0) )
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("A player is attempting to revote on a praxis.");
            }

            this._characterUpdate.AmendVotePointsLeft(
                recChar,
                new PointTotal(recChar.VotePointsLeft.Get + v.Points.Get)
            );
            try
            {
                this._voteRepo.Insert(v);
                this._characterRepo.Update(recChar);
                this._characterRepo.EndTransaction();
                return v;
            }
            catch (ArgumentException exc)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }

        /// <summary>
        /// Return the voting character associated with the supplied vote.
        /// </summary>
        private ICharacter _regGetCharacter(IVote v)
        {
            try
            {
                return this._characterRepo.GetById(v.CharacterId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("Could not insert the relation entity as its left ID is not registered with the correct repo.");
            }
        }

        private IPraxis _regGetPraxis(IPraxisParticipant pp)
        {
            try
            {
                return this._praxisRepo.GetById(pp.PraxisId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("Could not insert the relation entity as its right ID is not registered with the correct repo.");
            }
        }

        private IPraxisParticipant _regGetPP(IVote v)
        {
            try
            {
                return this._ppRepo.GetById(v.PraxisParticipantId);
            }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not find the corresponding praxis participant.", e);
            }
        }

        private ICharacter _regGetRecChar(IPraxisParticipant pp)
        {
            try
            {
                return this._characterRepo.GetById(pp.CharacterId);
            }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not find the receiving character.", e);
            }
        }

        /// <summary>
        /// Return the `Character.Id`s that the voting character's player
        /// contains.
        /// </summary>
        private IEnumerable<Id> _regGetVotersCharsIds(ICharacter votingChar)
        {
            IEnumerable<ICharacter> chars;
            try
            {
                chars = this._characterRepo.GetByPlayerId(votingChar.PlayerId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("There exists a vote with a player that has no characters.");
            }

            foreach (ICharacter c in chars)
                yield return c.Id;
        }

        /// <summary>
        /// Return the `Character.Id`s participating on the supplied praxis.
        /// </summary>
        private IEnumerable<Id> _regGetPraxisCharsIds(IPraxis praxis)
        {
            try
            {
                return this._ppRepo.GetCharIdsByPraxisId(praxis.Id);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("There exists a praxis that does not have any participants.");
            }
        }

        /// <summary>
        /// Return the `Character.Id`s of the voters for the related praxis.
        /// This returns null if there no results.
        /// </summary>
        /// <remarks>
        /// This can stand to be optimized - see the code for more.
        /// </remarks>
        private List<Id> _regGetPraxisVoters(IPraxisParticipant pp)
        {
            List<Id> voterIds = null;
            try
            {
                // OPTIMIZATION: create a single query instead of these two.
                var ppIds = this._ppRepo.GetIdsByPraxisId(pp.PraxisId);
                voterIds = this._voteRepo.GetCharacterIdsByPraxisParticipantIds
                    (ppIds.ToList()).ToList();
            }
            catch (ArgumentException)
            { }

            return voterIds;
        }
    }
}