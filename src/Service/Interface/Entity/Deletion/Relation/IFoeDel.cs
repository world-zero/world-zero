using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDel{TEntityRelation, TEntity, TId, TBuiltIn, TRelationDTO}"/>
    public interface IFoeDel
        : IEntitySelfRelationDel
        <IFoe, ICharacter, Id, int, RelationDTO<Id, int, Id, int>>
    {
        void DeleteByCharacter(ICharacter c);
        void DeleteByCharacter(Id charId);
        Task DeleteByCharacterAsync(ICharacter c);
        Task DeleteByCharacterAsync(Id charId);
    }
}