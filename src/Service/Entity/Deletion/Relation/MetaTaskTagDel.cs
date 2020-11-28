using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class MetaTaskTagDel : ITaggedEntityDel
    <
        MetaTaskTag,
        MetaTask,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public MetaTaskTagDel(IMetaTaskTagRepo repo)
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByMetaTask(MetaTask metaTask)
        {
            this.AssertNotNull(metaTask, "metaTask");
            this.DeleteByLeft(metaTask.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByMetaTask(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            this.DeleteByLeft(metaTaskId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByMetaTaskAsync(MetaTask metaTask)
        {
            this.AssertNotNull(metaTask, "metaTask");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeft(metaTask.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByMetaTaskAsync(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeft(metaTaskId));
        }
    }
}