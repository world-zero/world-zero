using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    public class VoteDel : IEntityDel<Vote, Id, int>
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        public VoteDel(IVoteRepo repo)
            : base(repo)
        { }

        public void DeleteByVotingChar(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByVotingChar(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingChar(c));
        }

        public void DeleteByVotingChar(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByVotingCharId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingChar(id));
        }

        public void DeleteByReceivingChar(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByReceivingChar(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingChar(c));
        }

        public void DeleteByReceivingChar(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByReceivingCharId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingChar(id));
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
            this.Transaction<Id>(this._voteRepo.DeleteByPraxisId, id);
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