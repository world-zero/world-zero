using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Flag")]
    /// <summary>
    /// Flag is a model for a tuple of the Flag table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Flag : IModel
    {
        [Key]
        /// <summary>
        /// FlagName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
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
        [NotMapped]
        private Name _flagName;

        /// <summary>
        /// Description is a description of the flag.
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
