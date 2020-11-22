using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Interface.Entity.Deletion
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
            IEntityRelationRepo
            <
                TEntityRelation,
                TLeftId,
                TLeftBuiltIn,
                Name,
                string,
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