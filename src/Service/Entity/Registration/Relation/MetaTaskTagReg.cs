using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class MetaTaskTagReg
        : IEntityRelationReg
        <
            MetaTaskTag,
            UnsafeMetaTask,
            Id,
            int,
            Tag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IMetaTaskTagRepo _metaTaskTagRepo
        { get { return (IMetaTaskTagRepo) this._repo; } }

        protected IUnsafeMetaTaskRepo _metaTaskRepo
        { get { return (IUnsafeMetaTaskRepo) this._leftRepo; } }

        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._rightRepo; } }

        public MetaTaskTagReg(
            IMetaTaskTagRepo metaTaskTagRepo,
            IUnsafeMetaTaskRepo metaTaskRepo,
            ITagRepo tagRepo
        )
            : base(metaTaskTagRepo, metaTaskRepo, tagRepo)
        { }
    }
}