using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <summary>
    /// As you migh expect, deleting a task will delete the Tags, Flags, and
    /// Praxises on that task - this involves using those corresponding
    /// deletion service classes.
    /// </summary>
    /// <inheritdoc cref="IIdStatusedEntityDel{TEntity}"/>
    public interface ITaskDel
        : IIdStatusedEntityDel<ITask>
    {
        void DeleteByFaction(IFaction f);
        void DeleteByFaction(Name factionId);
        Task DeleteByFactionAsync(IFaction f);
        Task DeleteByFactionAsync(Name factionId);
    }
}