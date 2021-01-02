using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ITaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public interface IMetaTaskTagDel
        : ITaggedEntityDel
        <
            IMetaTaskTag, IMetaTask, Id, int,
            RelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByMetaTask(IMetaTask mt);
        void DeleteByMetaTask(Id mtId);
        Task DeleteByMetaTaskAsync(IMetaTask mt);
        Task DeleteByMetaTaskAsync(Id mtId);
    }
}