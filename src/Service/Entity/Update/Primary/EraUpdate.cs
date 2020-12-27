using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IEraUpdate"/>
    public class EraUpdate : ABCEntityUpdate<IEra, Name, string>, IEraUpdate
    {
        public EraUpdate(IEraRepo repo)
            : base(repo)
        { }

        // --------------------------------------------------------------------

        public void AmendTaskLevelBuffer(IEra e, Level newTaskLevelBuffer)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newTaskLevelBuffer, "newTaskLevelBuffer");
            void f() => ((UnsafeEra) e).TaskLevelBuffer = newTaskLevelBuffer;
            this.AmendHelper(f, e);
        }

        public void AmendTaskLevelBuffer(Name eraId, Level newTaskLevelBuffer)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newTaskLevelBuffer, "newTaskLevelBuffer");
            void f()
            {
                var e = this._repo.GetById(eraId);
                this.AmendTaskLevelBuffer(e, newTaskLevelBuffer);
            }
            this.Transaction(f, true);
        }

        public async
        Task AmendTaskLevelBufferAsync(IEra e, Level newTaskLevelBuffer)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newTaskLevelBuffer, "newTaskLevelBuffer");
            await Task.Run(() =>
                this.AmendTaskLevelBuffer(e, newTaskLevelBuffer));
        }

        public async
        Task AmendTaskLevelBufferAsync(Name eraId, Level newTaskLevelBuffer)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newTaskLevelBuffer, "newTaskLevelBuffer");
            await Task.Run(() =>
                this.AmendTaskLevelBuffer(eraId, newTaskLevelBuffer));
        }

        // --------------------------------------------------------------------

        public void AmendMaxPraxises(IEra e, int newMaxPraxises)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxPraxises, "newMaxPraxises");
            void f() => ((UnsafeEra) e).MaxPraxises = newMaxPraxises;
            this.AmendHelper(f, e);
        }

        public void AmendMaxPraxises(Name eraId, int newMaxPraxises)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxPraxises, "newMaxPraxises");
            void f()
            {
                var e = this._repo.GetById(eraId);
                this.AmendMaxPraxises(e, newMaxPraxises);
            }
            this.Transaction(f, true);
        }

        public async Task AmendMaxPraxisesAsync(IEra e, int newMaxPraxises)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxPraxises, "newMaxPraxises");
            await Task.Run(() =>
                this.AmendMaxPraxises(e, newMaxPraxises));
        }

        public async Task AmendMaxPraxisesAsync(Name eraId, int newMaxPraxises)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxPraxises, "newMaxPraxises");
            await Task.Run(() =>
                this.AmendMaxPraxises(eraId, newMaxPraxises));
        }

        // --------------------------------------------------------------------

        public void AmendMaxTasks(IEra e, int newMaxTasks)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxTasks, "newMaxTasks");
            void f() => ((UnsafeEra) e).MaxTasks = newMaxTasks;
            this.AmendHelper(f, e);
        }

        public void AmendMaxTasks(Name eraId, int newMaxTasks)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxTasks, "newMaxTasks");
            void f()
            {
                var e = this._repo.GetById(eraId);
                this.AmendMaxTasks(e, newMaxTasks);
            }
            this.Transaction(f, true);
        }

        public async Task AmendMaxTasksAsync(IEra e, int newMaxTasks)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxTasks, "newMaxTasks");
            await Task.Run(() =>
                this.AmendMaxTasks(e, newMaxTasks));
        }

        public async Task AmendMaxTasksAsync(Name eraId, int newMaxTasks)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxTasks, "newMaxTasks");
            await Task.Run(() =>
                this.AmendMaxTasks(eraId, newMaxTasks));
        }

        // --------------------------------------------------------------------

        public void AmendMaxTasksReiterator(IEra e, int newMaxTasksReiterator)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxTasksReiterator, "newMaxTasksReiterator");
            void f() =>
                ((UnsafeEra) e).MaxTasksReiterator = newMaxTasksReiterator;
            this.AmendHelper(f, e);
        }

        public void
        AmendMaxTasksReiterator(Name eraId, int newMaxTasksReiterator)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxTasksReiterator, "newMaxTasksReiterator");
            void f()
            {
                var e = this._repo.GetById(eraId);
                this.AmendMaxTasksReiterator(e, newMaxTasksReiterator);
            }
            this.Transaction(f, true);
        }

        public async Task
        AmendMaxTasksReiteratorAsync(IEra e, int newMaxTasksReiterator)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newMaxTasksReiterator, "newMaxTasksReiterator");
            await Task.Run(() =>
                this.AmendMaxTasksReiterator(e, newMaxTasksReiterator));
        }

        public async Task
        AmendMaxTasksReiteratorAsync(Name eraId, int newMaxTasksReiterator)
        {
            this.AssertNotNull(eraId, "eraId");
            this.AssertNotNull(newMaxTasksReiterator, "newMaxTasksReiterator");
            await Task.Run(() =>
                this.AmendMaxTasksReiterator(eraId, newMaxTasksReiterator));
        }
    }
}