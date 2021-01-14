using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IFlaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public interface ITaskFlagDel
        : IFlaggedEntityDel
        <
            ITaskFlag, ITask, Id, int,
            NoIdRelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByTask(ITask t);
        void DeleteByTask(Id taskId);
        Task DeleteByTaskAsync(ITask t);
        Task DeleteByTaskAsync(Id taskId);
    }
}