using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class MetaTaskFlagDel : ABCFlaggedEntityDel
    <
        UnsafeMetaTaskFlag,
        UnsafeMetaTask,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public MetaTaskFlagDel(IMetaTaskFlagRepo repo)
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByMetaTask(UnsafeMetaTask metaTask)
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
        public async Task DeleteByMetaTaskAsync(UnsafeMetaTask metaTask)
        {
            this.AssertNotNull(metaTask, "metaTask");
            await Task.Run(() => this.DeleteByLeft(metaTask.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async Task DeleteByMetaTaskAsync(Id metaTaskId)
        {
            this.AssertNotNull(metaTaskId, "metaTaskId");
            await Task.Run(() => this.DeleteByLeft(metaTaskId));
        }
    }
}