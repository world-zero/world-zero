using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IFlaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public interface IMetaTaskFlagDel
        : IFlaggedEntityDel
        <
            IMetaTaskFlag, IMetaTask, Id, int,
            NoIdRelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByMetaTask(IMetaTask mt);
        void DeleteByMetaTask(Id mtId);
        Task DeleteByMetaTaskAsync(IMetaTask mt);
        Task DeleteByMetaTaskAsync(Id mtId);
    }
}