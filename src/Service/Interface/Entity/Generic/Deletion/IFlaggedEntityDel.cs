using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel"/>
    public abstract class IFlaggedEntityDel
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
        Flag,
        Name,
        string,
        TRelationDTO
    >
        where TEntityRelation : IFlaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        public IFlaggedEntityDel(
            IFlaggedEntityRepo
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
        public void DeleteByFlag(Flag flag)
        {
            this.AssertNotNull(flag, "flag");
            this.DeleteByRightId(flag.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        public void DeleteByFlag(Name flagId)
        {
            this.AssertNotNull(flagId, "flagId");
            this.DeleteByRightId(flagId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async System.Threading.Tasks.Task DeleteByFlagAsync(Flag flag)
        {
            this.AssertNotNull(flag, "flag");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByRightId(flag.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        public async System.Threading.Tasks.Task DeleteByFlagAsync(Name flagId)
        {
            this.AssertNotNull(flagId, "flagId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByRightId(flagId));
        }
    }
}