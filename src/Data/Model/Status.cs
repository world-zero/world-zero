using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Status")]
    /// <summary>
    /// Status is a model for a tuple of the Status table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Status : IModel
    {
        [Key]
        /// <summary>
        /// StatusName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
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
        [NotMapped]
        private Name _statusName;

        /// <summary>
        /// Description is a description of the status.
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}