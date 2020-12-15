using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IVoteDel"/>
    public class VoteDel
        : ABCEntityRelationDel
        <
            IVote,
            ICharacter, Id, int,
            IPraxisParticipant, Id, int,
            RelationDTO<Id, int, Id, int>
        >,
        IVoteDel
    {
        protected IVoteRepo _voteRepo
        { get { return (IVoteRepo) this._repo; } }

        public VoteDel(IVoteRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(ICharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public async Task DeleteByCharacterAsync(ICharacter c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByCharacter(c));
        }

        public void DeleteByCharacter(Id id)
        {
            this.DeleteByLeft(id);
        }

        public async Task DeleteByCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByCharacter(id));
        }

        public void DeleteByPraxisParticipant(IPraxisParticipant pp)
        {
            this.AssertNotNull(pp, "pp");
            this.DeleteByPraxisParticipant(pp.Id);
        }

        public async Task DeleteByPraxisParticipantAsync(
            IPraxisParticipant pp
        )
        {
            this.AssertNotNull(pp, "pp");
            await Task.Run(() => this.DeleteByPraxisParticipant(pp));
        }

        public void DeleteByPraxisParticipant(Id id)
        {
            this.DeleteByRight(id);
        }

        public async Task DeleteByPraxisParticipantAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByPraxisParticipant(id));
        }
    }
}