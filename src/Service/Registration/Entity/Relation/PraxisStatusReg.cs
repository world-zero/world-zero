using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class PraxisStatusReg
        : IEntityRelationReg
        <
            PraxisStatus,
            Praxis,
            Id,
            int,
            Status,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IPraxisStatusRepo _praxisStatusRepo
        { get { return (IPraxisStatusRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._rightRepo; } }

        public PraxisStatusReg(
            IPraxisStatusRepo praxisStatusRepo,
            IPraxisRepo praxisRepo,
            IStatusRepo statusRepo
        )
            : base(praxisStatusRepo, praxisRepo, statusRepo)
        { }
    }
}