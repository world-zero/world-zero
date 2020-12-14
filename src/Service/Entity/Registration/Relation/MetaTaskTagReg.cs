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
    /// <inheritdoc cref="IMetaTaskTagReg"/>
    public class MetaTaskTagReg
        : ABCEntityRelationReg
        <
            IMetaTaskTag,
            IMetaTask,
            Id,
            int,
            ITag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >, IMetaTaskTagReg
    {
        protected IMetaTaskTagRepo _metaTaskTagRepo
        { get { return (IMetaTaskTagRepo) this._repo; } }

        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._leftRepo; } }

        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._rightRepo; } }

        public MetaTaskTagReg(
            IMetaTaskTagRepo metaTaskTagRepo,
            IMetaTaskRepo metaTaskRepo,
            ITagRepo tagRepo
        )
            : base(metaTaskTagRepo, metaTaskRepo, tagRepo)
        { }
    }
}