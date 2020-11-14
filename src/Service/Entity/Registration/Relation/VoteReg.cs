using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    public class VoteReg
        : IEntityRelationReg
        <
            Vote,
            Character,
            Id,
            int,
            Praxis,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        /// <summary>
        /// As a character receives a vote, they will get this many votes.
        /// </summary>
        public static PointTotal VotesEarned = new PointTotal(2);

        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._leftRepo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._rightRepo; } }

        protected readonly IPraxisParticipantRepo _praxisPartRepo;

        public VoteReg(
            IVoteRepo voteRepo,
            ICharacterRepo characterRepo,
            IPraxisRepo praxisRepo,
            IPraxisParticipantRepo praxisParticipantRepo
        )
            : base(voteRepo, characterRepo, praxisRepo)
        {
            this.AssertNotNull(praxisParticipantRepo, "praxisParticipantRepo");
            this._praxisPartRepo = praxisParticipantRepo;
        }

        /// <remarks>
        /// This will ensure that a player isn't voting for their own praxis
        /// with a different character.
        /// </remarks>
        public override Vote Register(Vote v)
        {
            // This does not use base.PreRegisterChecks() as querying the two
            // repositories twice each would be extremely costly for a DB repo.
            this.AssertNotNull(v, "v");
            this._voteRepo.BeginTransaction(true);
            var votingChar     = this._regGetCharacter(v);
            var praxis         = this._regGetPraxis(v);
            var recChar        = this._regGetRecChar(v);
            var votersCharsIds = this._regGetVotersCharsIds(votingChar);
            var praxisCharsIds = this._regGetPraxisCharsIds(praxis);

            // Make sure someone isn't voting on their own praxis.
            if (votersCharsIds.Intersect(praxisCharsIds).Count() != 0)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("A player is attempting to vote on their own praxis.");
            }

            // Make sure someone isn't voting on a praxis multiple times.
            var votesCharIds = this._regGetPraxisVoters(v.PraxisId);
            if (   (votesCharIds != null)
                && (votersCharsIds.Intersect(votersCharsIds).Count() != 0) )
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("A player is attempting to revote on a praxis.");
            }

            recChar.VotePointsLeft = new PointTotal(
                recChar.VotePointsLeft.Get + VotesEarned.Get
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
        private Character _regGetCharacter(Vote v)
        {
            try
            {
                return this._characterRepo.GetById(v.VotingCharacterId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("Could not insert the relation entity as its left ID is not registered with the correct repo.");
            }
        }

        /// <summary>
        /// Make sure that `v.ReceivingCharacterId` is an actual character and
        /// that the character is a participant on `v.PraxisId`.
        /// </summary>
        private Character _regGetRecChar(Vote v)
        {
            // Check that the participant exists.
            Character c;
            try
            {
                c = this._characterRepo.GetById(v.ReceivingCharacterId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("The vote's receiving character does not exist.");
            }

            // Check that the participant actually participated.
            var pId = v.PraxisId;
            var cId = v.ReceivingCharacterId;
            if (!this._praxisPartRepo.ParticipantCheck(pId, cId))
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("The supplied vote's receiving character is not a participant of the supplied praxis.");
            }
            return c;
        }

        /// <summary>
        /// Return the praxis associated with the supplied vote.
        /// </summary>
        private Praxis _regGetPraxis(Vote v)
        {
            try
            {
                return this._praxisRepo.GetById(v.RightId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new ArgumentException("Could not insert the relation entity as its right ID is not registered with the correct repo.");
            }
        }

        /// <summary>
        /// Return the `Character.Id`s that the voting character's player
        /// contains.
        /// </summary>
        private IEnumerable<Id> _regGetVotersCharsIds(Character votingChar)
        {
            IEnumerable<Character> chars;
            try
            {
                chars = this._characterRepo.GetByPlayerId(votingChar.PlayerId);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("There exists a vote with a player that has no characters.");
            }

            foreach (Character c in chars)
                yield return c.Id;
        }

        /// <summary>
        /// Return the `Character.Id`s associated with the supplied praxis.
        /// </summary>
        private IEnumerable<Id> _regGetPraxisCharsIds(Praxis praxis)
        {
            try
            {
                return this._praxisPartRepo.GetCharIdsByPraxisId(praxis.Id);
            }
            catch (ArgumentException)
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("There exists a praxis that does not have any participants.");
            }
        }

        /// <summary>
        /// Return the `Character.Id`s that have voting on `v.PraxisId`. This
        /// will return null if no one has voted on the praxis before.
        /// </summary>
        private IEnumerable<Id> _regGetPraxisVoters(Id praxisId)
        {
            try
            {
                if (this._voteRepo == null)
                    throw new InvalidOperationException("_voteRepo");
                return this._voteRepo.GetPraxisVoters(praxisId);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}