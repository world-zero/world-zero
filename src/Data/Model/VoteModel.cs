using WorldZero.Common.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Vote")]
    public class VoteModel : IModel
    {
        [Key, Column(Order=1)]
        public virtual int PraxisId { get; set; }
        [ForeignKey("PraxisId")]
        public virtual PraxisModel Praxis { get; set; }

        [Key, Column(Order=2)]
        public virtual int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }

        [Required]
        public int Points { get; set; }
    }
}