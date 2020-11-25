using System;
using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion
{
    public class MetaTaskUnset
        : IEntityUnset<MetaTask, Id, int, Praxis, Id, int>
    {
        public IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._otherRepo; } }

        public MetaTaskUnset(IMetaTaskRepo repo, IPraxisRepo praxisRepo)
            : base(repo, praxisRepo)
        { }

        public override void Unset(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            this.BeginTransaction();

            IEnumerable<Praxis> praxiss;
            try
            { praxiss = this._praxisRepo.GetByMetaTaskId(metaTaskId); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the unset.", e);
            }

            foreach (Praxis p in praxiss)
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

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}