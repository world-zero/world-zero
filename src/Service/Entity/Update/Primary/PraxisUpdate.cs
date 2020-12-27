using System;
using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IPraxisUpdate"/>
    public class PraxisUpdate
        : ABCIdStatusedEntityUpdate<IPraxis>, IPraxisUpdate
    {
        protected readonly IPraxisParticipantRepo _ppRepo;
        protected readonly IMetaTaskRepo _mtRepo;

        public PraxisUpdate(
            IPraxisRepo repo,
            IPraxisParticipantRepo ppRepo,
            IStatusRepo statusRepo,
            IMetaTaskRepo mtRepo
        )
            : base(repo, statusRepo)
        {
            this.AssertNotNull(ppRepo, "ppRepo");
            this.AssertNotNull(mtRepo, "mtRepo");
            this._ppRepo = ppRepo;
            this._mtRepo = mtRepo;
        }

        // --------------------------------------------------------------------

        public void AmendPoints(IPraxis p, PointTotal newPoints)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newPoints, "newPoints");
            void f() => ((UnsafePraxis) p).Points = newPoints;
            this.AmendHelper(f, p);
        }

        public void AmendPoints(Id praxisId, PointTotal newPoints)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.AssertNotNull(newPoints, "newPoints");
            void f()
            {
                var p = this._repo.GetById(praxisId);
                this.AmendPoints(p, newPoints);
            }
            this.Transaction(f, true);
        }

        public async Task AmendPointsAsync(IPraxis p, PointTotal newPoints)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newPoints, "newPoints");
            await Task.Run(() => this.AmendPoints(p, newPoints));
        }

        public async Task AmendPointsAsync(Id praxisId, PointTotal newPoints)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.AssertNotNull(newPoints, "newPoints");
            await Task.Run(() => this.AmendPoints(praxisId, newPoints));
        }

        // --------------------------------------------------------------------

        public void AmendMetaTask(IPraxis p, IMetaTask newMT)
        {
            this.AssertNotNull(p, "p");
            void f()
            {
                Id newMTId = null;
                if (newMT != null)
                {
                    newMTId = newMT.Id;
                    this._mtRepo.GetById(newMTId);
                }
                ((UnsafePraxis) p).MetaTaskId = newMTId;
            }
            this.AmendHelper(f, p, true);
        }

        public void AmendMetaTask(IPraxis p, Id newMTId)
        {
            this.AssertNotNull(p, "p");
            void f()
            {
                if (newMTId != null)
                    this._mtRepo.GetById(newMTId);
                ((UnsafePraxis) p).MetaTaskId = newMTId;
            }
            this.AmendHelper(f, p, true);
        }

        public void AmendMetaTask(Id praxisId, IMetaTask newMT)
        {
            this.AssertNotNull(praxisId, "praxisId");
            void f()
            {
                this.AmendMetaTask(this._repo.GetById(praxisId), newMT);
            }
            this.Transaction(f, true);
        }

        public void AmendMetaTask(Id praxisId, Id newMTId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            void f()
            {
                this.AmendMetaTask(this._repo.GetById(praxisId), newMTId);
            }
            this.Transaction(f, true);
        }

        public async Task AmendMetaTaskAsync(IPraxis p, IMetaTask newMT)
        {
            this.AssertNotNull(p, "p");
            await Task.Run(() => this.AmendMetaTask(p, newMT));
        }

        public async Task AmendMetaTaskAsync(IPraxis p, Id newMTId)
        {
            this.AssertNotNull(p, "p");
            await Task.Run(() => this.AmendMetaTask(p, newMTId));
        }

        public async Task AmendMetaTaskAsync(Id praxisId, IMetaTask newMT)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.AmendMetaTask(praxisId, newMT));
        }

        public async Task AmendMetaTaskAsync(Id praxisId, Id newMTId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.AmendMetaTask(praxisId, newMTId));
        }

        // --------------------------------------------------------------------

        public void AmendAreDueling(IPraxis p, bool newAreDueling)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newAreDueling, "newAreDueling");
            void f()
            {
                if (  (newAreDueling)
                    &&(this._ppRepo.GetParticipantCountViaPraxisId(p.Id) != 2))
                {
                    throw new ArgumentException("Could not set the praxis to a duel, there is not exactly two participants.");
                }
                ((UnsafePraxis) p).AreDueling = newAreDueling;
            }
            this.AmendHelper(f, p, true);
        }

        public void AmendAreDueling(Id praxisId, bool newAreDueling)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.AssertNotNull(newAreDueling, "newAreDueling");
            void f()
            {
                var p = this._repo.GetById(praxisId);
                this.AmendAreDueling(p, newAreDueling);
            }
            this.Transaction(f, true);
        }

        public async Task
        AmendAreDuelingAsync(IPraxis p, bool newAreDueling)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newAreDueling, "newAreDueling");
            await Task.Run(() =>
                this.AmendAreDueling(p, newAreDueling));
        }

        public async Task
        AmendAreDuelingAsync(Id praxisId, bool newAreDueling)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.AssertNotNull(newAreDueling, "newAreDueling");
            await Task.Run(() =>
                this.AmendAreDueling(praxisId, newAreDueling));
        }
    }
}