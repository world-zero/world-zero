using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Player")]
    /// <summary>
    /// Player is a entity for a tuple of the Player table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Player : INamedEntity
    {
        [Required]
        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        internal virtual ICollection<Character> Characters { get; set; }
    }
}