using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Player")]
    public class PlayerModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        [Required, StringLength(25)]
        // Also is Unique - enforced via Fluent API.
        public string Username { get; set; }
        [Required]
        public bool IsBlocked { get; set; } = false;

        public virtual ICollection<CharacterModel> Characters { get; set; }
    }
}