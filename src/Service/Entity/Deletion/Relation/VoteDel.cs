using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <remarks>
    /// This will not refund the vote points used.
    /// </remarks>
    public class VoteDel : IEntityDel<UnsafeVote, Id, int>
    {
        protected IUnsafeVoteRepo _voteRepo
        { get { return (IUnsafeVoteRepo) this._repo; } }

        public VoteDel(IUnsafeVoteRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterAsync(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(c));
        }

        public void DeleteByCharacter(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByCharacterId, id);
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(id));
        }

        public void DeleteByPraxisParticipant(UnsafePraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            this.DeleteByPraxisParticipant(pp.Id);
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisParticipantAsync(
            UnsafePraxisParticipant pp
        )
        {
            this.AssertNotNull(pp, "pp");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxisParticipant(pp));
        }

        public void DeleteByPraxisParticipant(Id id)
        {
            this.Transaction<Id>(
                this._voteRepo.DeleteByPraxisParticipantId,
                id
            );
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisParticipantAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxisParticipant(id));
        }
    }
}