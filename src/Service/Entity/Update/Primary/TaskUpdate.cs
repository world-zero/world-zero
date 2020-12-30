using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="ITaskUpdate"/>
    public class TaskUpdate
        : ABCIdStatusedEntityUpdate<ITask>, ITaskUpdate
    {
        protected readonly IFactionRepo _factionRepo;

        public TaskUpdate(
            ITaskRepo repo,
            IStatusRepo statusRepo,
            IFactionRepo factionRepo
        )
            : base(repo, statusRepo)
        {
            this.AssertNotNull(factionRepo, "factionRepo");
            this._factionRepo = factionRepo;
        }

        // --------------------------------------------------------------------

        public void AmendSummary(ITask t, string newSummary)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newSummary, "newSummary");
            void f() => ((UnsafeTask) t).Summary = newSummary;
            this.AmendHelper(f, t);
        }

        public void AmendSummary(Id taskId, string newSummary)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newSummary, "newSummary");
            void f() =>
                this.AmendSummary(this._repo.GetById(taskId), newSummary);
            this.Transaction(f, true);
        }

        public async Task AmendSummaryAsync(ITask t, string newSummary)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newSummary, "newSummary");
            await Task.Run(() => this.AmendSummary(t, newSummary));
        }

        public async Task AmendSummaryAsync(Id taskId, string newSummary)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newSummary, "newSummary");
            await Task.Run(() => this.AmendSummary(taskId, newSummary));
        }

        // --------------------------------------------------------------------

        public void AmendFaction(ITask t, IFaction newFaction)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newFaction, "newFaction");
            void f()
            {
                Name newFactionId = newFaction.Id;
                this._factionRepo.GetById(newFactionId);
                ((UnsafeTask) t).FactionId = newFactionId;
            }
            this.AmendHelper(f, t, true);
        }

        public void AmendFaction(ITask t, Name newFactionId)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newFactionId, "newFactionId");
            void f()
            {
                this._factionRepo.GetById(newFactionId);
                ((UnsafeTask) t).FactionId = newFactionId;
            }
            this.AmendHelper(f, t, true);
        }

        public void AmendFaction(Id taskId, IFaction newFaction)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newFaction, "newFaction");
            void f()
            {
                this.AmendFaction(this._repo.GetById(taskId), newFaction);
            }
            this.Transaction(f, true);
        }

        public void AmendFaction(Id taskId, Name newFactionId)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newFactionId, "newFactionId");
            void f()
            {
                this.AmendFaction(this._repo.GetById(taskId), newFactionId);
            }
            this.Transaction(f, true);
        }

        public async Task AmendFactionAsync(ITask t, IFaction newFaction)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newFaction, "newFaction");
            await Task.Run(() => this.AmendFaction(t, newFaction));
        }

        public async Task AmendFactionAsync(ITask t, Name newFactionId)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newFactionId, "newFactionId");
            await Task.Run(() => this.AmendFaction(t, newFactionId));
        }

        public async Task AmendFactionAsync(Id taskId, IFaction newFaction)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newFaction, "newFaction");
            await Task.Run(() => this.AmendFaction(taskId, newFaction));
        }

        public async Task AmendFactionAsync(Id taskId, Name newFactionId)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newFactionId, "newFactionId");
            await Task.Run(() => this.AmendFaction(taskId, newFactionId));
        }

        // --------------------------------------------------------------------

        public void AmendPoints(ITask t, PointTotal newPoints)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newPoints, "newPoints");
            void f() => ((UnsafeTask) t).Points = newPoints;
            this.AmendHelper(f, t);
        }

        public void AmendPoints(Id taskId, PointTotal newPoints)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newPoints, "newPoints");
            void f()
            {
                var p = this._repo.GetById(taskId);
                this.AmendPoints(p, newPoints);
            }
            this.Transaction(f, true);
        }

        public async Task AmendPointsAsync(ITask t, PointTotal newPoints)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newPoints, "newPoints");
            await Task.Run(() => this.AmendPoints(t, newPoints));
        }

        public async Task AmendPointsAsync(Id taskId, PointTotal newPoints)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newPoints, "newPoints");
            await Task.Run(() => this.AmendPoints(taskId, newPoints));
        }

        // --------------------------------------------------------------------

        public void AmendLevel(ITask t, Level newLevel)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newLevel, "newLevel");
            void f() => ((UnsafeTask) t).Level = newLevel;
            this.AmendHelper(f, t);
        }

        public void AmendLevel(Id taskId, Level newLevel)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newLevel, "newLevel");
            void f()
            {
                var p = this._repo.GetById(taskId);
                this.AmendLevel(p, newLevel);
            }
            this.Transaction(f, true);
        }

        public async Task AmendLevelAsync(ITask t, Level newLevel)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newLevel, "newLevel");
            await Task.Run(() => this.AmendLevel(t, newLevel));
        }

        public async Task AmendLevelAsync(Id taskId, Level newLevel)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newLevel, "newLevel");
            await Task.Run(() => this.AmendLevel(taskId, newLevel));
        }

        // --------------------------------------------------------------------

        public void AmendMinLevel(ITask t, Level newMinLevel)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newMinLevel, "newMinLevel");
            void f() => ((UnsafeTask) t).MinLevel = newMinLevel;
            this.AmendHelper(f, t);
        }

        public void AmendMinLevel(Id taskId, Level newMinLevel)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newMinLevel, "newMinLevel");
            void f()
            {
                var p = this._repo.GetById(taskId);
                this.AmendMinLevel(p, newMinLevel);
            }
            this.Transaction(f, true);
        }

        public async Task AmendMinLevelAsync(ITask t, Level newMinLevel)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newMinLevel, "newMinLevel");
            await Task.Run(() => this.AmendMinLevel(t, newMinLevel));
        }

        public async Task AmendMinLevelAsync(Id taskId, Level newMinLevel)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newMinLevel, "newMinLevel");
            await Task.Run(() => this.AmendMinLevel(taskId, newMinLevel));
        }

        // --------------------------------------------------------------------

        public void AmendIsHistorianable(ITask t, bool newIsHistorianable)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newIsHistorianable, "newIsHistorianable");
            void f() => ((UnsafeTask) t).IsHistorianable = newIsHistorianable;
            this.AmendHelper(f, t);
        }

        public void AmendIsHistorianable(Id taskId, bool newIsHistorianable)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newIsHistorianable, "newIsHistorianable");
            void f()
            {
                var p = this._repo.GetById(taskId);
                this.AmendIsHistorianable(p, newIsHistorianable);
            }
            this.Transaction(f, true);
        }

        public async Task
        AmendIsHistorianableAsync(ITask t, bool newIsHistorianable)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newIsHistorianable, "newIsHistorianable");
            await Task.Run(() =>
                this.AmendIsHistorianable(t, newIsHistorianable));
        }

        public async Task
        AmendIsHistorianableAsync(Id taskId, bool newIsHistorianable)
        {
            this.AssertNotNull(taskId, "taskId");
            this.AssertNotNull(newIsHistorianable, "newIsHistorianable");
            await Task.Run(() =>
                this.AmendIsHistorianable(taskId, newIsHistorianable));
        }
    }
}