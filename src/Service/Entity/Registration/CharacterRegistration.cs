using System;
using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class CharacterRegistration
        : IEntityRegistration<Character, Id, int>
    {
        protected readonly IPlayerRepo _playerRepo;
        protected readonly IFactionRepo _factionRepo;
        protected readonly ILocationRepo _locationRepo;

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._repo; } }

        public CharacterRegistration(
            ICharacterRepo characterRepo,
            IPlayerRepo playerRepo,
            IFactionRepo factionRepo,
            ILocationRepo locationRepo
        )
            : base(characterRepo)
        {
            if (playerRepo == null) throw new ArgumentNullException("playerRepo");
            if (factionRepo == null) throw new ArgumentNullException("factionRepo");
            if (locationRepo == null) throw new ArgumentNullException("locationRepo");
            this._playerRepo = playerRepo;
            this._factionRepo = factionRepo;
            this._locationRepo = locationRepo;
        }

        /// <summary>
        /// Create the character and save them. This will ensure that the
        /// character has a valid player ID, and faction ID if set.
        /// </summary>
        public override Character Register(Character c)
        {
            this.AssertNotNull(c);
            try
            {
                this._playerRepo.GetById(c.PlayerId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Character of ID {c.Id} has an invalid Player ID of {c.PlayerId}.");
            }
            try
            {
                if (c.FactionId != null)
                    this._factionRepo.GetById(c.FactionId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Character of ID {c.Id} has an invalid Faction ID of {c.FactionId}.");
            }
            try
            {
                if (c.LocationId != null)
                    this._locationRepo.GetById(c.LocationId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Character of ID {c.Id} has an invalid Location ID of {c.LocationId}.");
            }
            return base.Register(c);
        }
    }
}