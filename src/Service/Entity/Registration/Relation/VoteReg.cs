using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    /// <remarks>
    /// This will immediately award the participant with the amount of
    /// `Vote.Points` to their `Character.VotePointsLeft` field. This does not
    /// use the Character updating service class.
    /// <br />
    /// Naturally, Players cannot vote for themselves, and Players cannot vote
    /// several times on a praxis. Note that this describes a Player.
    /// </remarks>
    public class VoteReg
        : ABCEntityRelationReg
        <
            UnsafeVote,
            UnsafeCharacter,
            Id,
            int,
            UnsafePraxisParticipant,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._leftRepo; } }

        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._rightRepo; } }

        protected readonly IPraxisRepo _praxisRepo;

        public VoteReg(
            IVoteRepo voteRepo,
            ICharacterRepo characterRepo,
            IPraxisParticipantRepo praxisParticipantRepo,
            IPraxisRepo praxisRepo
        )
            : base(voteRepo, characterRepo, praxisParticipantRepo)
        {
            this.AssertNotNull(praxisRepo, "praxisRepo");
            this._praxisRepo = praxisRepo;
        }

        public override UnsafeVote Register(UnsafeVote v)
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

            recChar.VotePointsLeft = new PointTotal(
                recChar.VotePointsLeft.Get + v.Points.Get
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
        private UnsafeCharacter _regGetCharacter(UnsafeVote v)
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

        private UnsafePraxis _regGetPraxis(UnsafePraxisParticipant pp)
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

        private UnsafePraxisParticipant _regGetPP(UnsafeVote v)
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

        private UnsafeCharacter _regGetRecChar(UnsafePraxisParticipant pp)
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
        private IEnumerable<Id> _regGetVotersCharsIds(UnsafeCharacter votingChar)
        {
            IEnumerable<UnsafeCharacter> chars;
            try
            {
                chars = this._characterRepo.GetByPlayerId(votingChar.PlayerId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("There exists a vote with a player that has no characters.");
            }

            foreach (UnsafeCharacter c in chars)
                yield return c.Id;
        }

        /// <summary>
        /// Return the `Character.Id`s participating on the supplied praxis.
        /// </summary>
        private IEnumerable<Id> _regGetPraxisCharsIds(UnsafePraxis praxis)
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
        private List<Id> _regGetPraxisVoters(UnsafePraxisParticipant pp)
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