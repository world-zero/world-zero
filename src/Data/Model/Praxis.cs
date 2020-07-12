using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Praxis")]
    public class Praxis : IModel
    {
        [NotMapped]
        private Id _praxisId;
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
        private Id _taskId;
        // Pretend this is required.
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
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        [NotMapped]
        private bool _AreDueling = false;
        [Required]
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
        private Name _statusName;
        [Required]
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
        [ForeignKey("StatusName")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Character> Collaborators { get; set; }
        public virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}