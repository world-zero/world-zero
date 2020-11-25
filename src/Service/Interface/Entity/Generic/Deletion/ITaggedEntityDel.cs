using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

// TODO: make ITaggedEntity / IFlaggedEntity and adjust entities/repos/dels to need that too

// TODO: update this to use ITaggedEntity
// TODO: copy this logic to IFlaggedEntityDel
//      then fill out the repo deletion wrappers

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel"/>
    public abstract class ITaggedEntityDel
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
        Tag,
        Name,
        string,
        TRelationDTO
    >
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, Name, string>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        public ITaggedEntityDel(
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
        public void DeleteByTag(Tag tag)
        {
            this.AssertNotNull(tag, "tag");
            this.DeleteByRightId(tag.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        public void DeleteByTag(Name tagName)
        {
            this.AssertNotNull(tagName, "tagName");
            this.DeleteByRightId(tagName);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async System.Threading.Tasks.Task DeleteByTagAsync(Tag tag)
        {
            this.AssertNotNull(tag, "tag");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByRightId(tag.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async System.Threading.Tasks.Task DeleteByTagAsync(Name tagName)
        {
            this.AssertNotNull(tagName, "tagName");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByRightId(tagName));
        }
    }
}