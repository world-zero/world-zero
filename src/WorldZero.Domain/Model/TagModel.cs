using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Tag")]
    public class TagModel
    {
        [Key]
        public string TagName { get; set; }
        public string description { get; set; }
    }
}
