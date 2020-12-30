using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface ITaggedEntityDel
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRelationDTO
    >
        : IEntityRelationDel
        <
            TEntityRelation,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            ITag,
            Name,
            string,
            TRelationDTO
        >
        where TEntityRelation : class, ITaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        void DeleteByTag(ITag tag);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        void DeleteByTag(Name tagId);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        Task DeleteByTagAsync(ITag tag);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        Task DeleteByTagAsync(Name tagId);
    }
}