using System.Threading.Tasks;
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
    public class VoteDel : ABCEntityDel<UnsafeVote, Id, int>
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        public VoteDel(IVoteRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public async Task DeleteByCharacterAsync(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByCharacter(c));
        }

        public void DeleteByCharacter(Id id)
        {
            this.Transaction<Id>(this._voteRepo.DeleteByCharacterId, id);
        }

        public async Task DeleteByCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByCharacter(id));
        }

        public void DeleteByPraxisParticipant(UnsafePraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            this.DeleteByPraxisParticipant(pp.Id);
        }

        public async Task DeleteByPraxisParticipantAsync(
            UnsafePraxisParticipant pp
        )
        {
            this.AssertNotNull(pp, "pp");
            await Task.Run(() => this.DeleteByPraxisParticipant(pp));
        }

        public void DeleteByPraxisParticipant(Id id)
        {
            this.Transaction<Id>(
                this._voteRepo.DeleteByPraxisParticipantId,
                id
            );
        }

        public async Task DeleteByPraxisParticipantAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByPraxisParticipant(id));
        }
    }
}