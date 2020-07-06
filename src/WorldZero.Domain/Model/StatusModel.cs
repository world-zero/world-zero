using WorldZero.Domain.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Status")]
    public class StatusModel : IModel
    {
        [Key]
        public string StatusName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
        public virtual ICollection<PraxisModel> Praxises { get; set; }
        public virtual ICollection<MetaTaskModel> MetaTasks { get; set; }
    }
}