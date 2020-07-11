using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Player")]
    public class PlayerModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        [Required, StringLength(25)]
        // Also is Unique - enforced via Fluent API.
        public string Username { get; set; }
        [Required]
        public bool IsBlocked { get; set; } = false;

        public virtual ICollection<Character> Characters { get; set; }
    }
}