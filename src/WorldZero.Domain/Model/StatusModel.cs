using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Status")]
    public class StatusModel
    {
        [Key]
        public string StatusName { get; set; }
        public string description { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}