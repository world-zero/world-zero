using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ITaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public interface IPraxisTagDel
        : ITaggedEntityDel
        <
            IPraxisTag, IPraxis, Id, int,
            RelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByPraxis(IPraxis praxis);
        void DeleteByPraxis(Id praxisId);
        Task DeleteByPraxisAsync(IPraxis praxis);
        Task DeleteByPraxisAsync(Id praxisId);
    }
}