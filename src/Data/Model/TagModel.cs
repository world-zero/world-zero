using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Tag")]
    public class TagModel : IModel
    {
        [Key]
        public string TagName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
        public virtual ICollection<PraxisModel> Praxises { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
