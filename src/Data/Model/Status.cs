using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Status")]
    public class Status : IModel
    {
        [NotMapped]
        private Name _statusName;
        [Key]
        public string StatusName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._statusName,
                    null);
            }
            set { this._statusName = new Name(value); }
        }

        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}