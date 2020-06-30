using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Character")]
    public class CharacterModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        [Required]
        public string Displayname { get; set; }

        [Required]
        public virtual int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual PlayerModel Player { get; set; }

        [Required]
        public int EraPoints { get; set; }
        [Required]
        public int TotalPoints { get; set; }
        [Required]
        public int EraLevel { get; set; }
        [Required]
        public int TotalLevel { get; set; }

        public virtual int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationModel Location { get; set; }

        // These relations are handled via Flutent API.
        public virtual ICollection<CharacterModel> Friends { get; set; }
        public virtual ICollection<CharacterModel> Foes { get; set; }
    }
}