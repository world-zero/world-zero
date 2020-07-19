using WorldZero.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Tag")]
    public class Tag : INamedEntity
    {
        /// <summary>
        /// Description is a description of the tag.
        /// </summary>
        public string Description { get; set; }

        internal virtual ICollection<Task> Tasks { get; set; }
        internal virtual ICollection<Praxis> Praxises { get; set; }
        internal virtual ICollection<MetaTask> MetaTasks { get; set; }
    }
}
