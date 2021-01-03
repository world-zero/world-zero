using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <summary>
    /// Create the character and save them. This will ensure that the
    /// character has a valid player ID, that the character's faction and
    /// location are correct (as appropriate), and that the player can create
    /// (another) character.
    /// </summary>
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface ICharacterReg
        : IEntityReg<ICharacter, Id, int>
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
        /// <br />
        /// Sure, this may "technically" be breaking the abstraction principle,
        /// but bite me - this could be fix if there was a not-nullable
        /// reference type.
        /// </remarks>
        static Level MinLevelToRegister
        {
            get { return _minLevel; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MinLevelToRegister");
                _minLevel = value;
            }
        }
        private static Level _minLevel { get; set; }

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
        bool CanRegCharacter(IPlayer p);

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
        bool CanRegCharacter(Id playerId);
    }
}