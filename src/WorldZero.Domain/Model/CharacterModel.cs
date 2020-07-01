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
        [Required]
        public int VotePointsLeft { get; set; }

        // This needs to be nullable so that EF Core will allow this to be
        // a not non-null field.
        public virtual int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual LocationModel Location { get; set; }

        public virtual string FactionName { get; set; }
        [ForeignKey("FactionName")]
        public virtual FactionModel Faction { get; set; }

        // These relations are handled via Fluent API.
        public virtual ICollection<CharacterModel> Friends { get; set; }
        public virtual ICollection<CharacterModel> Foes { get; set; }
    }
}