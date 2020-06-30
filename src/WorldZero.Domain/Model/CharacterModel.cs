using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Character")]
    public class CharacterModel
    {
        [Key, Column(Order=0)]
        public string Displayname { get; set; }

        [Key, Column(Order=1)]
        public virtual string Username { get; set; }
        [ForeignKey("Username")]
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