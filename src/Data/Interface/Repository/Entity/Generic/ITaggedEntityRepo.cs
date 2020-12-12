using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <remarks>
    /// As you may have noticed, relational entities that tag should have the
    /// tag ID as the right relation.
    /// </remarks>
    public interface ITaggedEntityRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRelationDTO
    >
        : IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            Name,
            string,
            TRelationDTO
        >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, Name, string>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        // NOTE: I am leaving the GetByTagId until I work on the reading
        // service classes.

        /// <summary>
        /// `Delete()` the relations with the supplied flag.
        /// </summary>
        void DeleteByTagId(Name tagId);
        Task DeleteByTagIdAsync(Name tagId);
    }
}