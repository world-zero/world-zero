using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <remarks>
    /// This will not allow a praxis to be transfered to be for a different
    /// task.
    /// <br />
    /// This will not allow a praxis to become a duel if there is not exaclty
    /// two participants.
    /// </remarks>
    /// <inheritdoc cref="IIdStatusedEntityUpdate{TEntity}"/>
    public interface IPraxisUpdate : IIdStatusedEntityUpdate<IPraxis>
    {
        void AmendPoints(IPraxis p, PointTotal newPoints);
        void AmendPoints(Id praxisId, PointTotal newPoints);
        Task AmendPointsAsync(IPraxis p, PointTotal newPoints);
        Task AmendPointsAsync(Id praxisId, PointTotal newPoints);

        void AmendMetaTask(IPraxis p, IMetaTask newMetaTask);
        void AmendMetaTask(IPraxis p, Id newMetaTaskId);
        void AmendMetaTask(Id praxisId, IMetaTask newMetaTask);
        void AmendMetaTask(Id praxisId, Id newMetaTaskId);
        Task AmendMetaTaskAsync(IPraxis p, IMetaTask newMetaTask);
        Task AmendMetaTaskAsync(IPraxis p, Id newMetaTaskId);
        Task AmendMetaTaskAsync(Id praxisId, IMetaTask newMetaTask);
        Task AmendMetaTaskAsync(Id praxisId, Id newMetaTaskId);

        void AmendAreDueling(IPraxis p, bool newAreDueling);
        void AmendAreDueling(Id praxisId, bool newAreDueling);
        Task AmendAreDuelingAsync(IPraxis p, bool newAreDueling);
        Task AmendAreDuelingAsync(Id praxisId, bool newAreDueling);
    }
}