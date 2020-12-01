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

        public void DeleteByVotingCharacter(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByVotingCharacter(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharacterAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingCharacter(c));
        }

        public void DeleteByVotingCharacter(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByVotingCharId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByVotingCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByVotingCharacter(id));
        }

        public void DeleteByReceivingCharacter(Character c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByReceivingCharacter(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharacterAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingCharacter(c));
        }

        public void DeleteByReceivingCharacter(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByReceivingCharId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByReceivingCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByReceivingCharacter(id));
        }

        public void DeleteByPraxis(Praxis p)
        {
            this.AssertNotNull(p, "p");
            this.DeleteByPraxis(p.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Praxis p)
        {
            this.AssertNotNull(p, "p");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(p));
        }

        public void DeleteByPraxis(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByPraxisId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(id));
        }
    }
}