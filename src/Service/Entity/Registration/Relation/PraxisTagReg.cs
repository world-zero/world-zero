using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class PraxisTagReg
        : IEntityRelationReg
        <
            UnsafePraxisTag,
            UnsafePraxis,
            Id,
            int,
            UnsafeTag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IUnsafePraxisTagRepo _praxisTagRepo
        { get { return (IUnsafePraxisTagRepo) this._repo; } }

        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._leftRepo; } }

        protected IUnsafeTagRepo _tagRepo
        { get { return (IUnsafeTagRepo) this._rightRepo; } }

        public PraxisTagReg(
            IUnsafePraxisTagRepo praxisTagRepo,
            IUnsafePraxisRepo praxisRepo,
            IUnsafeTagRepo tagRepo
        )
            : base(praxisTagRepo, praxisRepo, tagRepo)
        { }
    }
}