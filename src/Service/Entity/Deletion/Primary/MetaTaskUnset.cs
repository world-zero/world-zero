using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset"/>
    /// <remarks>
    /// This can also perform `IIdStatusedEntityDel` operations. Sure, this is
    /// definitely a little smelly, but eh.
    /// </remarks>
    public class MetaTaskUnset
        : IEntityUnset<MetaTask, Id, int, Praxis, Id, int>
    {
        protected class StatusedMTDel
            : IIdStatusedEntityDel<MetaTask>
        {
            public StatusedMTDel(IMetaTaskRepo mtRepo)
                : base(mtRepo)
            { }
        }

        protected StatusedMTDel _statusedMTDel;

        protected IMetaTaskRepo _mtRepo
        { get { return (IMetaTaskRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._otherRepo; } }

        public MetaTaskUnset(IMetaTaskRepo repo, IPraxisRepo praxisRepo)
            : base(repo, praxisRepo)
        {
            this._statusedMTDel = new StatusedMTDel(this._mtRepo);
        }

        public void DeleteByStatus(Status s)
        {
            this._statusedMTDel.DeleteByStatus(s);
        }

        public void DeleteByStatus(Name statusId)
        {
            this._statusedMTDel.DeleteByStatus(statusId);
        }

        public async
        System.Threading.Tasks.Task DeleteByStatusAsync(Status s)
        {
            this.AssertNotNull(s, "s");
            await System.Threading.Tasks.Task.Run(() =>
                this._statusedMTDel.DeleteByStatus(s));
        }

        public async
        System.Threading.Tasks.Task DeleteByStatusAsync(Name statusId)
        {
            this.AssertNotNull(statusId, "statusId");
            await System.Threading.Tasks.Task.Run(() =>
                this._statusedMTDel.DeleteByStatus(statusId));
        }

        public override void Unset(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            this.BeginTransaction();

            IEnumerable<Praxis> praxises = null;
            try
            { praxises = this._praxisRepo.GetByMetaTaskId(metaTaskId); }
            catch (ArgumentException)
            { }

            if (praxises != null)
            {
                foreach (Praxis p in praxises)
                {
                    p.MetaTaskId = null;
                    try
                    { this._praxisRepo.Update(p); }
                    catch (ArgumentException e)
                    {
                        this.DiscardTransaction();
                        throw new ArgumentException("Could not complete the unset.", e);
                    }
                }
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }

        public void DeleteByFaction(UnsafeFaction f)
        {
            this.AssertNotNull(f, "f");
            this.DeleteByFaction(f.Id);
        }

        public void DeleteByFaction(Name factionId)
        {
            void f(Name factionName)
            {
                IEnumerable<MetaTask> mts;
                try
                { mts = this._mtRepo.GetByFactionId(factionName); }
                catch (ArgumentException)
                { return; }

                foreach (MetaTask mt in mts)
                    this.Delete(mt);
            }

            this.Transaction<Name>(f, factionId, true);
        }

        public async
        System.Threading.Tasks.Task DeleteByFactionAsync(UnsafeFaction f)
        {
            this.AssertNotNull(f, "f");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByFaction(f));
        }

        public async
        System.Threading.Tasks.Task DeleteByFactionAsync(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByFaction(factionId));
        }
    }
}
