using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Flag")]
    public class Flag : IModel
    {
        [NotMapped]
        private Name _flagName;
        [Key]
        public string FlagName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._flagName,
                    null);
            }
            set { this._flagName = new Name(value); }
        }
        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
