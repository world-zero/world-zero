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
    public class MetaTaskTagReg
        : IEntityRelationReg
        <
            UnsafeMetaTaskTag,
            UnsafeMetaTask,
            Id,
            int,
            UnsafeTag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IUnsafeMetaTaskTagRepo _metaTaskTagRepo
        { get { return (IUnsafeMetaTaskTagRepo) this._repo; } }

        protected IUnsafeMetaTaskRepo _metaTaskRepo
        { get { return (IUnsafeMetaTaskRepo) this._leftRepo; } }

        protected IUnsafeTagRepo _tagRepo
        { get { return (IUnsafeTagRepo) this._rightRepo; } }

        public MetaTaskTagReg(
            IUnsafeMetaTaskTagRepo metaTaskTagRepo,
            IUnsafeMetaTaskRepo metaTaskRepo,
            IUnsafeTagRepo tagRepo
        )
            : base(metaTaskTagRepo, metaTaskRepo, tagRepo)
        { }
    }
}