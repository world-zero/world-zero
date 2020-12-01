using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface ITaskRepo
        : IIdStatusedEntityRepo<Task>
    {
        /// <summary>
        /// Return a collection of tasks that are owned by the corresponding
        /// faction. If none exist, an exception is thrown.
        /// </summary>
        IEnumerable<Task> GetByFactionId(Name factionId);
    }
}