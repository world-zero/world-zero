using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class CharacterReg
        : IEntityReg<Character, Id, int>
    {
        /// <summary>
        /// This level controls the minimum level at which a player can create
        /// additional characters.
        /// <summary>
        /// </remarks>
        /// Naturally, if a player has no characters, then they can register a
        /// new character. Alternatively, a player's level is determined by
        /// looping through each of their characters and finding the
        /// character(s) with the highest era or total level, the larger of
        /// which is used for the player level.
        /// </remarks>
        public static Level MinLevelToRegister = new Level(3);

        /// <summary>
        /// Determine if a player can register another character based off
        /// MinLevelToRegister.
        /// </summary>
        /// </remarks>
        /// Naturally, if a player has no characters, then they can register a
        /// new character. Alternatively, a player's level is determined by
        /// looping through each of their characters and finding the
        /// character(s) with the highest era or total level, the larger of
        /// which is used for the player level.
        /// </remarks>
        public bool CanRegCharacter(Player p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            return CanRegCharacter(p.Id);
        }
        /// <summary>
        /// Determine if a player can register another character based off
        /// MinLevelToRegister.
        /// </summary>
        /// </remarks>
        /// Naturally, if a player has no characters, then they can register a
        /// new character. Alternatively, a player's level is determined by
        /// looping through each of their characters and finding the
        /// character(s) with the highest era or total level, the larger of
        /// which is used for the player level.
        /// </remarks>
        public bool CanRegCharacter(Id playerId)
        {
            if (playerId == null)
                throw new ArgumentNullException("playerId");
            if (MinLevelToRegister == null)
                throw new ArgumentException("MinLevelToRegister is null, but it is needed for this method.");

            try
            {
                Level l = this._characterRepo.FindHighestLevel(playerId);
                if (l.Get < MinLevelToRegister.Get)
                    return false;
            }
            catch (ArgumentException)
            { }
            return true;
        }

        protected readonly IPlayerRepo _playerRepo;
        protected readonly IFactionRepo _factionRepo;
        protected readonly ILocationRepo _locationRepo;

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._repo; } }

        public CharacterReg(
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
            this.AssertNotNull(c, "c");
            this._characterRepo.BeginTransaction(true);
            this._verifyPlayer(c);
            this._verifyFaction(c);
            this._verifyLocation(c);
            try
            {
                var r = base.Register(c);
                this._characterRepo.EndTransaction();
                return r;
            }
            catch (ArgumentException exc)
            {
                this._characterRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }

        private void _verifyPlayer(Character c)
        {
            bool shouldCrash = false;
            try
            {
                Player p = this._playerRepo.GetById(c.PlayerId);
                if (!this.CanRegCharacter(p))
                    shouldCrash = true;
            }
            catch (ArgumentNullException)
            {
                this._characterRepo.DiscardTransaction();
                throw new InvalidOperationException("A null was found where it should not be possible.");
            }
            catch (ArgumentException)
            {
                this._characterRepo.DiscardTransaction();
                throw new ArgumentException($"Character of ID {c.Id.Get} has an invalid Player ID of {c.PlayerId.Get}.");
            }

            if (shouldCrash)
            {
                this._characterRepo.DiscardTransaction();
                throw new ArgumentException("The supplied Character belongs to a Player that does not have sufficient level to register another Character.");
            }
        }

        private void _verifyFaction(Character c)
        {
            try
            {
                if (c.FactionId != null)
                    this._factionRepo.GetById(c.FactionId);
            }
            catch (ArgumentException)
            {
                this._characterRepo.DiscardTransaction();
                throw new ArgumentException($"Character of ID {c.Id.Get} has an invalid Faction ID of {c.FactionId.Get}.");
            }
        }

        private void _verifyLocation(Character c)
        {
            try
            {
                if (c.LocationId != null)
                    this._locationRepo.GetById(c.LocationId);
            }
            catch (ArgumentException)
            {
                this._characterRepo.DiscardTransaction();
                throw new ArgumentException($"Character of ID {c.Id.Get} has an invalid Location ID of {c.LocationId.Get}.");
            }
        }
    }
}