using WorldZero.Common.Interface.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Flag")]
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Flag : INamedEntity
    {
        /// <summary>
        /// Description is a description of the flag.
        /// </summary>
        public string Description { get; set; }

        internal virtual ICollection<Task> Tasks { get; set; }
        internal virtual ICollection<Praxis> Praxises { get; set; }
        internal virtual ICollection<Comment> Comments { get; set; }
        internal virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
