using System;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    public class VoteDel : IEntityDel<Vote, Id, int>
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        public VoteDel(IVoteRepo repo)
            : base(repo)
        { }

        public void DeleteByVotingCharId(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByVotingCharId(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingCharId(c));
        }

        public void DeleteByVotingCharId(Id id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._voteRepo.DeleteByVotingCharId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not complete the deletion.", e); }
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingCharId(id));
        }

        public void DeleteByReceivingCharId(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByReceivingCharId(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingCharId(c));
        }

        public void DeleteByReceivingCharId(Id id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._voteRepo.DeleteByReceivingCharId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not complete the deletion.", e); }
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingCharId(id));
        }

        public void DeleteByPraxisId(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByPraxisId(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisIdAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxisId(c));
        }

        public void DeleteByPraxisId(Id id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._voteRepo.DeleteByPraxisId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not complete the deletion.", e); }
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisIdAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxisId(id));
        }
    }
}