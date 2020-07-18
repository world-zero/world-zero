using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("Tag")]
    public class Tag : IModel
    {
        [Key]
        /// <summary>
        /// TagName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
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
        [NotMapped]
        private Name _tagName;

        /// <summary>
        /// Description is a description of the tag.
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
