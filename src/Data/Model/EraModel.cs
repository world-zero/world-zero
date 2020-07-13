using WorldZero.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Era")]
    /// <summary>
    /// Era is a model for a tuple of the Era table.
    /// </summary>
    public class EraModel : IModel
    {
        [Key]
        /// <summary>
        /// EraName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string EraName { get; set; }

        [Required]
        /// <summary>
        /// StartDate is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// EndDate is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}