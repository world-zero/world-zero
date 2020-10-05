using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class MetaTaskStatusReg
        : IEntityRelationReg
        <
            MetaTaskStatus,
            MetaTask,
            Id,
            int,
            Status,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IMetaTaskStatusRepo _metaTaskStatusRepo
        { get { return (IMetaTaskStatusRepo) this._repo; } }

        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._leftRepo; } }

        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._rightRepo; } }

        public MetaTaskStatusReg(
            IMetaTaskStatusRepo metaTaskStatusRepo,
            IMetaTaskRepo metaTaskRepo,
            IStatusRepo statusRepo
        )
            : base(metaTaskStatusRepo, metaTaskRepo, statusRepo)
        { }
    }
}