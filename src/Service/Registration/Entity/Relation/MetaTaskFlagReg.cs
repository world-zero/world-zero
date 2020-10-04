using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class MetaTaskFlagReg
        : IEntityRelationReg
        <
            MetaTaskFlag,
            MetaTask,
            Id,
            int,
            Flag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IMetaTaskFlagRepo _metaTaskFlagRepo
        { get { return (IMetaTaskFlagRepo) this._repo; } }

        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public MetaTaskFlagReg(
            IMetaTaskFlagRepo metaTaskFlagRepo,
            IMetaTaskRepo metaTaskRepo,
            IFlagRepo flagRepo
        )
            : base(metaTaskFlagRepo, metaTaskRepo, flagRepo)
        { }
    }
}