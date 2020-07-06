using WorldZero.Domain.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Location")]
    public class LocationModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zip { get; set; }

        public virtual ICollection<CharacterModel> Characters { get; set; }
    }
}