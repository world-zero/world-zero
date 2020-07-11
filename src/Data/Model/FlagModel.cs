using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Flag")]
    public class FlagModel : IModel
    {
        [Key]
        public string FlagName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
        public virtual ICollection<PraxisModel> Praxises { get; set; }
        public virtual ICollection<CommentModel> Comments { get; set; }
        public virtual ICollection<MetaTaskModel> MetaTasks { get; set; }
    }
}
