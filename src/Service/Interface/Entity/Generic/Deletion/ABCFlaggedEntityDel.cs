using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IFlaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public abstract class ABCFlaggedEntityDel
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
            IFlag,
            Name,
            string,
            TRelationDTO
        >,
        IFlaggedEntityDel
        <
            TEntityRelation,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            TRelationDTO
        >
        where TEntityRelation : class, IFlaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        public ABCFlaggedEntityDel(
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

        public void DeleteByFlag(IFlag flag)
        {
            this.AssertNotNull(flag, "flag");
            this.DeleteByRight(flag.Id);
        }

        public void DeleteByFlag(Name flagId)
        {
            this.AssertNotNull(flagId, "flagId");
            this.DeleteByRight(flagId);
        }

        public async Task DeleteByFlagAsync(IFlag flag)
        {
            this.AssertNotNull(flag, "flag");
            await Task.Run(() => this.DeleteByRight(flag.Id));
        }

        public async Task DeleteByFlagAsync(Name flagId)
        {
            this.AssertNotNull(flagId, "flagId");
            await Task.Run(() => this.DeleteByRight(flagId));
        }
    }
}