using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IMetaTaskFlagDel"/>
    public class MetaTaskFlagDel : ABCFlaggedEntityDel
    <
        IMetaTaskFlag,
        IMetaTask,
        Id,
        int,
        NoIdRelationDTO<Id, int, Name, string>
    >, IMetaTaskFlagDel
    {
        public MetaTaskFlagDel(IMetaTaskFlagRepo repo)
            : base(repo)
        { }

        public void DeleteByMetaTask(IMetaTask metaTask)
        {
            this.AssertNotNull(metaTask, "metaTask");
            this.DeleteByLeft(metaTask.Id);
        }

        public void DeleteByMetaTask(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            this.DeleteByLeft(metaTaskId);
        }

        public async Task DeleteByMetaTaskAsync(IMetaTask metaTask)
        {
            this.AssertNotNull(metaTask, "metaTask");
            await Task.Run(() => this.DeleteByLeft(metaTask.Id));
        }

        public async Task DeleteByMetaTaskAsync(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            await Task.Run(() => this.DeleteByLeft(metaTaskId));
        }
    }
}