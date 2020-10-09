using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

// TODO: make sure a player is voting once on a praxis

// TODO: Upon receiveing a vote, give 2 votes to the character.
//      Points are given as votes are recieved, apart from duels. I am waiting on further information about duels, and how long to keep a praxis live.

namespace WorldZero.Service.Registration.Entity.Relation
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
            if (this._voteRepo == null) throw new InvalidOperationException("voterepo");
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
            var votingChar     = this._regGetCharacter(v);
            var praxis         = this._regGetPraxis(v);
            var votersCharsIds = this._regGetVotersCharsIds(votingChar);
            var praxisCharsIds = this._regGetPraxisCharsIds(praxis);

            if (votersCharsIds.Intersect(praxisCharsIds).Count() != 0)
                throw new ArgumentException("A player is attempting to vote on their own praxis.");

            var votesCharIds = this._regGetPraxisVoters(v.PraxisId);
            if (   (votesCharIds != null)
                && (votersCharsIds.Intersect(votersCharsIds).Count() != 0) )
            {
                throw new ArgumentException("A player is attempting to revote on a praxis.");
            }

            return base.Register(v);
        }

        /// <summary>
        /// Return the character associated with the supplied vote.
        /// </summary>
        private Character _regGetCharacter(Vote v)
        {
            try
            {
                return this._leftRepo.GetById(v.LeftId);
            }
            catch (ArgumentException)
            { throw new ArgumentException("Could not insert the relation entity as its left ID is not registered with the correct repo."); }
        }

        /// <summary>
        /// Return the praxis associated with the supplied vote.
        /// </summary>
        private Praxis _regGetPraxis(Vote v)
        {
            try
            {
                return this._rightRepo.GetById(v.RightId);
            }
            catch (ArgumentException)
            { throw new ArgumentException("Could not insert the relation entity as its right ID is not registered with the correct repo."); }
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
            { throw new InvalidOperationException("There exists a vote with a player that has no characters."); }

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
            { throw new InvalidOperationException("There exists a praxis that does not have any participants."); }
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