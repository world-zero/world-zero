using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <summary>
    /// As you migh expect, deleting a praxis will delete the Comments, Tags,
    /// Flags, and Participants (and Votes by extension) on that praxis.
    /// </summary>
    /// <remarks>
    /// This will also delete the participant's received votes. Whether or not
    /// the voting character will receive a refund is up to <see
    /// cref="VoteDel"/>.
    /// <br />
    /// Dev note: shockingly, deleting a praxis has no special logic outside of
    /// cascading the deletion.
    /// </remarks>
    /// <inheritdoc cref="IIdStatusedEntityDel{TEntity}"/>
    public interface IPraxisDel
        : IIdStatusedEntityDel<IPraxis>
    {
        void DeleteByTask(ITask t);
        void DeleteByTask(Id taskId);
        Task DeleteByTaskAsync(ITask t);
        Task DeleteByTaskAsync(Id taskId);
    }
}