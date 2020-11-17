using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Registration.Relation
{
    public class VoteDel : IEntityDel<Vote, Id, int>
    {
        public VoteDel(IVoteRepo repo)
            : base(repo)
        { }
    }
}