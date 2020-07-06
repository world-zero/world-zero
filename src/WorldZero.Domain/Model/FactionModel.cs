using WorldZero.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Faction")]
    public class FactionModel : IModel
    {
        [Key]
        public string FactionName { get; set; }

        public DateTime DateFounded { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public string AbilityName { get; set; }
        public string AbilityDesc { get; set; }

        public virtual ICollection<CharacterModel> Members { get; set; }
        public virtual ICollection<TaskModel> SponsoredTasks { get; set; }
    }
}