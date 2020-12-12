using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="ITaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public abstract class ABCTaggedEntityDel
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRelationDTO
    >
        : ABCEntityRelationDel
        <
            TEntityRelation,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            ITag,
            Name,
            string,
            TRelationDTO
        >,
        ITaggedEntityDel
        <
            TEntityRelation,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            TRelationDTO
        >
        where TEntityRelation : class, ITaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        public ABCTaggedEntityDel(
            ITaggedEntityRepo
            <
                TEntityRelation,
                TLeftId,
                TLeftBuiltIn,
                TRelationDTO
            >
            repo
        )
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        public void DeleteByTag(ITag tag)
        {
            this.AssertNotNull(tag, "tag");
            this.DeleteByRight(tag.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        public void DeleteByTag(Name tagId)
        {
            this.AssertNotNull(tagId, "tagId");
            this.DeleteByRight(tagId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async Task DeleteByTagAsync(ITag tag)
        {
            this.AssertNotNull(tag, "tag");
            await Task.Run(() => this.DeleteByRight(tag.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async Task DeleteByTagAsync(Name tagId)
        {
            this.AssertNotNull(tagId, "tagId");
            await Task.Run(() => this.DeleteByRight(tagId));
        }
    }
}