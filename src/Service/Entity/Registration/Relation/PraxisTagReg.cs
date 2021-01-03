using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IPraxisTagReg"/>
    public class PraxisTagReg
        : ABCEntityRelationReg
        <
            IPraxisTag,
            IPraxis,
            Id,
            int,
            ITag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >, IPraxisTagReg
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