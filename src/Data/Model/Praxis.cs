using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Praxis")]
    /// <summary>
    /// Praxis is a model for a tuple of the Praxis table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Praxis : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int PraxisId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._praxisId,
                    0);
            }
            set { this._praxisId = new Id(value); }
        }
        [NotMapped]
        private Id _praxisId;

        // Pretend this is required.
        /// <summary>
        /// TaskId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int? TaskId
        {
            get
            {
                var r = this.Eval<int>(
                    (ISingleValueObject<int>) this._taskId,
                    -1);
                if (r == -1) return null;
                else         return r;
            }
            set
            {
                if (value == null) this._taskId = null;
                else               this._taskId = new Id((int) value);
            }
        }
        [NotMapped]
        private Id _taskId;

        [ForeignKey("TaskId")]
        /// <summary>
        /// The <c>Task</c> that this <c>Praxis</c> completes.
        /// </summary>
        public virtual Task Task { get; set; }

        [Required]
        /// <summary>
        /// AreDueling controls whether or not the collaborator(s) are dueling.
        /// If there is not two collaborators, this will default to false.
        /// </summary>
        public virtual bool AreDueling
        {
            get { return this._AreDueling; }
            set
            {
                if ( (this.Collaborators == null)
                        || (this.Collaborators.Count != 2) )
                    this._AreDueling = false;
                else
                    this._AreDueling = value;
            }
        }
        [NotMapped]
        private bool _AreDueling = false;

        [Required]
        /// <summary>
        /// StatusName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string StatusName
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

        [ForeignKey("StatusName")]
        /// <summary>
        /// The <c>Status</c> that this <c>Praxis</c> has.
        /// </summary>
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Character> Collaborators { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}