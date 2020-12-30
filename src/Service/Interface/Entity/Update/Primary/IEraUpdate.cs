using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <remarks>
    /// It is extremely not recommended to expose this anywhere but for the
    /// <see cref="CzarConsole"/>.
    /// <br />
    /// This cannot edit end or start dates - use <see
    /// cref="WorldZero.Service.Interface.Entity.Registration.Primary.IEraReg"/>
    /// for that services.
    /// </remarks>
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IEraUpdate : IEntityUpdate<IEra, Name, string>
    {
        void AmendTaskLevelBuffer(IEra e, Level newTaskLevelBuffer);
        void AmendTaskLevelBuffer(Name eraId, Level newTaskLevelBuffer);
        Task AmendTaskLevelBufferAsync(IEra e, Level newTaskLevelBuffer);
        Task AmendTaskLevelBufferAsync(Name eraId, Level newTaskLevelBuffer);

        void AmendMaxPraxises(IEra e, int newMaxPraxises);
        void AmendMaxPraxises(Name eraId, int newMaxPraxises);
        Task AmendMaxPraxisesAsync(IEra e, int newMaxPraxises);
        Task AmendMaxPraxisesAsync(Name eraId, int newMaxPraxises);

        void AmendMaxTasks(IEra e, int newMaxTasks);
        void AmendMaxTasks(Name eraId, int newMaxTasks);
        Task AmendMaxTasksAsync(IEra e, int newMaxTasks);
        Task AmendMaxTasksAsync(Name eraId, int newMaxTasks);

        void AmendMaxTasksReiterator(IEra e, int newMaxTasksReiterator);
        void AmendMaxTasksReiterator(Name eraId, int newMaxTasksReiterator);
        Task AmendMaxTasksReiteratorAsync(IEra e, int newMaxTasksReiterator);
        Task AmendMaxTasksReiteratorAsync(Name eraId, int newMaxTasksReiterator);
    }
}