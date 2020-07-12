using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Task")]
    public class TaskModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        public string Summary { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int Level { get; set; }
        public int? MinLevel { get; set; }

        [Required]
        public virtual string FactionName { get; set; }
        [ForeignKey("FactionName")]
        public virtual Faction Faction { get; set; }

        [Required]
        public virtual string StatusName { get; set; }
        [ForeignKey("StatusName")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
    }
}