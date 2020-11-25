using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
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