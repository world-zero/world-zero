using WorldZero.Domain.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Praxis")]
    public class PraxisModel : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PraxisId { get; set; }

        public virtual int? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskModel Task { get; set; }

        [Required]
        public virtual bool IsDueling { get; set; } = false;

        [Required]
        public virtual string StatusName { get; set; }
        [ForeignKey("StatusName")]
        public virtual StatusModel Status { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual ICollection<FlagModel> Flags { get; set; }
        public virtual ICollection<CharacterModel> Collaborators { get; set; }
        public virtual ICollection<MetaTaskModel> MetaTasks { get; set; }
    }
}