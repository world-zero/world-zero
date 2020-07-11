using WorldZero.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Comment")]
    public class CommentModel : IModel
    {
        [Key, Column(Order=1)]
        public virtual int PraxisId { get; set; }
        [ForeignKey("PraxisId")]
        public virtual PraxisModel Praxis { get; set; }

        [Key, Column(Order=2)]
        public virtual int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public virtual CharacterModel Character { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public virtual ICollection<FlagModel> Flags { get; set; }
    }
}