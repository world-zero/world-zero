using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Tag")]
    public class Tag : IModel
    {
        [NotMapped]
        private Name _tagName;
        [Key]
        public string TagName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._tagName,
                    null);
            }
            set { this._tagName = new Name(value); }
        }
        public string Description { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
