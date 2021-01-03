using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IIdStatusedEntityUpdate{TEntity}"/>
    public interface ITaskUpdate : IIdStatusedEntityUpdate<ITask>
    {
        void AmendSummary(ITask t, string newSummary);
        void AmendSummary(Id taskId, string newSummary);
        Task AmendSummaryAsync(ITask t, string newSummary);
        Task AmendSummaryAsync(Id taskId, string newSummary);

        void AmendFaction(ITask t, IFaction newFaction);
        void AmendFaction(ITask t, Name newFactionId);
        void AmendFaction(Id taskId, IFaction newFaction);
        void AmendFaction(Id taskId, Name newFactionId);
        Task AmendFactionAsync(ITask t, IFaction newFaction);
        Task AmendFactionAsync(ITask t, Name newFactionId);
        Task AmendFactionAsync(Id taskId, IFaction newFaction);
        Task AmendFactionAsync(Id taskId, Name newFactionId);

        void AmendPoints(ITask t, PointTotal newPoints);
        void AmendPoints(Id taskId, PointTotal newPoints);
        Task AmendPointsAsync(ITask t, PointTotal newPoints);
        Task AmendPointsAsync(Id taskId, PointTotal newPoints);

        void AmendLevel(ITask t, Level newLevel);
        void AmendLevel(Id taskId, Level newLevel);
        Task AmendLevelAsync(ITask t, Level newLevel);
        Task AmendLevelAsync(Id taskId, Level newLevel);

        void AmendMinLevel(ITask t, Level newMinLevel);
        void AmendMinLevel(Id taskId, Level newMinLevel);
        Task AmendMinLevelAsync(ITask t, Level newMinLevel);
        Task AmendMinLevelAsync(Id taskId, Level newMinLevel);

        void AmendIsHistorianable(ITask t, bool newIsHistorianable);
        void AmendIsHistorianable(Id taskId, bool newIsHistorianable);
        Task AmendIsHistorianableAsync(ITask t, bool newIsHistorianable);
        Task AmendIsHistorianableAsync(Id taskId, bool newIsHistorianable);
    }
}