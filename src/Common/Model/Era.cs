using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("Era")]
    /// <summary>
    /// Era is a model for a tuple of the Era table.
    /// </summary>
    public class Era : IModel
    {
        [Key]
        /// <summary>
        /// EraName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string EraName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._eraName,
                    null);
            }
            set { this._eraName = new Name(value); }
        }
        [NotMapped]
        private Name _eraName;


        [Required]
        /// <summary>
        /// StartDate is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return this.Eval<DateTime>(
                    (ISingleValueObject<DateTime>) this._startDate,
                    DateTime.UtcNow);
            }
            set
            {
                var start = new PastDate(value);
                this._checkDates(start, this._endDate);
                this._startDate = start;
            }
        }
        [NotMapped]
        private PastDate _startDate;

        /// <summary>
        /// EndDate is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
        public DateTime? EndDate
        {
            get
            {
                var r = this.Eval<DateTime>(
                    (ISingleValueObject<DateTime>) this._endDate,
                    new DateTime(1000, 1, 1));
                if (r == new DateTime(1000, 1, 1)) return null;
                else                               return this._endDate.Get;
            }
            set
            {
                if (value == null)
                    this._endDate = null;
                else
                {
                    var end = new PastDate((DateTime) value);
                    this._checkDates(this._startDate, end);
                    this._endDate = end;
                }
            }
        }
        [NotMapped]
        private PastDate _endDate;

        private void _checkDates(PastDate start, PastDate end)
        {
            if ( (start != null) 
                && (end != null)
                && (end.Get < start.Get) )
            {
                throw new ArgumentException("The end date must be after the start date.");
            }
        }
    }
}