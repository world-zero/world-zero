using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Domain.Model
{
    [Table("Flag")]
    public class FlagModel
    {
        [Key]
        public string FlagName { get; set; }
        public string description { get; set; }
    }
}
