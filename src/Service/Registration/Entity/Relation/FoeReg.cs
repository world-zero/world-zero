using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class FoeReg
        : IEntityRelationReg
        <
            Foe,
            Character,
            Id,
            int,
            Character,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IFoeRepo _foeRepo
        { get { return (IFoeRepo) this._repo; } }

        public FoeReg(
            IFoeRepo foeRepo,
            ICharacterRepo leftCharacterRepo,
            ICharacterRepo rightCharacterRepo
        )
            : base(foeRepo, leftCharacterRepo, rightCharacterRepo)
        { }
    }
}