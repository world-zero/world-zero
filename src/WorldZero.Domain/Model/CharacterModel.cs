using WorldZero.Domain.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Character")]
    public class CharacterModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        [Required]
        public string Displayname { get; set; }

        public bool HasBio { get; set; } = false;
        public bool HasProfilePic { get; set; } = false;

        [Required]
        public virtual int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual PlayerModel Player { get; set; }

        [Required]
        public int EraPoints { get; set; } = 0;
        [Required]
        public int TotalPoints { get; set; } = 0;
        [Required]
        public int EraLevel { get; set; } = 0;
        [Required]
        public int TotalLevel { get; set; } = 0;
        [Required]
        public int VotePointsLeft { get; set; } = 100;

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

        public virtual ICollection<PraxisModel> Praxises { get; set; }
    }
}