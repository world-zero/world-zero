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
    }
}