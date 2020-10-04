using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class FriendReg
        : IEntityRelationReg
        <
            Friend,
            Character,
            Id,
            int,
            Character,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IFriendRepo _friendRepo
        { get { return (IFriendRepo) this._repo; } }

        public FriendReg(
            IFriendRepo friendRepo,
            ICharacterRepo leftCharacterRepo,
            ICharacterRepo rightCharacterRepo
        )
            : base(friendRepo, leftCharacterRepo, rightCharacterRepo)
        { }
    }
}