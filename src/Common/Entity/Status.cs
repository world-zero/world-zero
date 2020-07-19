using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Status")]
    /// <summary>
    /// Status is a entity for a tuple of the Status table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Status : INamedEntity
    {
        /// <summary>
        /// Description is a description of the status.
        /// </summary>
        public string Description { get; set; }

        internal virtual ICollection<Task> Tasks { get; set; }
        internal virtual ICollection<Praxis> Praxises { get; set; }
        internal virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}