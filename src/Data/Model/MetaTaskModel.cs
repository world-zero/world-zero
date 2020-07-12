using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("MetaTask")]
    public class MetaTaskModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MetaTaskId { get; set; }

        [Required]
        public string MetaTaskName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Bonus { get; set; }
        [Required]
        public bool IsFlatBonus { get; set; } = true;

        [Required]
        public virtual string FactionName { get; set; }
        [ForeignKey("FactionName")]
        public virtual Faction Faction { get; set; }

        // Pretend this is required.
        public virtual string StatusName { get; set; }
        [ForeignKey("StatusName")]
        public virtual StatusModel Status { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual ICollection<FlagModel> Flags { get; set; }
        public virtual ICollection<PraxisModel> Praxises { get; set; }
    }
}