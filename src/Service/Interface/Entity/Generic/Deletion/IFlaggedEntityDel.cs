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
    public interface IFlaggedEntityDel
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
            IFlag,
            Name,
            string,
            TRelationDTO
        >
        where TEntityRelation : class, IFlaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        void DeleteByFlag(IFlag flag);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightId()`.
        /// </remarks>
        void DeleteByFlag(Name flagId);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        Task DeleteByFlagAsync(IFlag flag);

        /// <remarks>
        /// This is just a wrapper for `DeleteByRightIdAsync()`.
        /// </remarks>
        Task DeleteByFlagAsync(Name flagId);
    }
}