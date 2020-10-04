using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class PraxisFlagReg
        : IEntityRelationReg
        <
            PraxisFlag,
            Praxis,
            Id,
            int,
            Flag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IPraxisFlagRepo _praxisFlagRepo
        { get { return (IPraxisFlagRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public PraxisFlagReg(
            IPraxisFlagRepo praxisFlagRepo,
            IPraxisRepo praxisRepo,
            IFlagRepo flagRepo
        )
            : base(praxisFlagRepo, praxisRepo, flagRepo)
        { }
    }
}