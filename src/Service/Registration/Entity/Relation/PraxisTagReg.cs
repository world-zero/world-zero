using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class PraxisTagReg
        : IEntityRelationReg
        <
            PraxisTag,
            Praxis,
            Id,
            int,
            Tag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IPraxisTagRepo _praxisTagRepo
        { get { return (IPraxisTagRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._rightRepo; } }

        public PraxisTagReg(
            IPraxisTagRepo praxisTagRepo,
            IPraxisRepo praxisRepo,
            ITagRepo tagRepo
        )
            : base(praxisTagRepo, praxisRepo, tagRepo)
        { }
    }
}