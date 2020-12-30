using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Deletion.Primary;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IMetaTaskUnset"/>
    public class MetaTaskUnset
        : ABCEntityUnset<IMetaTask, Id, int, IPraxis, Id, int>, IMetaTaskUnset
    {
        protected class StatusedMTDel
            : ABCIdStatusedEntityDel<IMetaTask>
        {
            public StatusedMTDel(IMetaTaskRepo mtRepo)
                : base(mtRepo)
            { }
        }

        protected StatusedMTDel _twin;


        protected IMetaTaskRepo _mtRepo
        { get { return (IMetaTaskRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._otherRepo; } }

        protected IPraxisUpdate _praxisUpdate
        { get { return (IPraxisUpdate) this._otherUpdate; } }


        public MetaTaskUnset(
            IMetaTaskRepo repo,
            IPraxisRepo praxisRepo,
            IPraxisUpdate praxisUpdate
        )
            : base(repo, praxisRepo, praxisUpdate)
        {
            this._twin = new StatusedMTDel(this._mtRepo);
        }

        public void DeleteByStatus(IStatus s)
        {
            this._twin.DeleteByStatus(s);
        }

        public void DeleteByStatus(Name statusId)
        {
            this._twin.DeleteByStatus(statusId);
        }

        public async Task DeleteByStatusAsync(IStatus s)
        {
            this.AssertNotNull(s, "s");
            await Task.Run(() => this._twin.DeleteByStatus(s));
        }

        public async Task DeleteByStatusAsync(Name statusId)
        {
            this.AssertNotNull(statusId, "statusId");
            await Task.Run(() => this._twin.DeleteByStatus(statusId));
        }

        public override void Unset(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            this.BeginTransaction();

            IEnumerable<IPraxis> praxises = null;
            try
            { praxises = this._praxisRepo.GetByMetaTaskId(metaTaskId); }
            catch (ArgumentException)
            { }

            if (praxises != null)
            {
                Id mt = null;
                foreach (IPraxis p in praxises)
                    this._praxisUpdate.AmendMetaTask(p, mt);
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }

        public void DeleteByFaction(IFaction f)
        {
            this.AssertNotNull(f, "f");
            this.DeleteByFaction(f.Id);
        }

        public void DeleteByFaction(Name factionId)
        {
            void f(Name factionName)
            {
                IEnumerable<IMetaTask> mts;
                try
                { mts = this._mtRepo.GetByFactionId(factionName); }
                catch (ArgumentException)
                { return; }

                foreach (IMetaTask mt in mts)
                    this.Delete(mt);
            }

            this.Transaction<Name>(f, factionId, true);
        }

        public async Task DeleteByFactionAsync(IFaction f)
        {
            this.AssertNotNull(f, "f");
            await Task.Run(() => this.DeleteByFaction(f));
        }

        public async Task DeleteByFactionAsync(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            await Task.Run(() => this.DeleteByFaction(factionId));
        }
    }
}
