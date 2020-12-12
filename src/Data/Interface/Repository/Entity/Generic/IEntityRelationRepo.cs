using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <remarks>
    /// It is recommended to make methods that have better names than left or
    /// right ID that are wrappers to the corresponding method.
    /// </remarks>
    public interface IEntityRelationRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IIdEntityRepo<TEntityRelation>
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        /// <summary>
        /// Return the saved relational entities that have the supplied left
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByLeftId(TLeftId id);

        /// <summary>
        /// Return the saved relational entities that have the supplied right
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByRightId(TRightId id);

        /// <summary>
        /// Return the saved IDs of the entities with the corresponding left
        /// ID. If there are none, then this throws an exception.
        /// </summary>
        IEnumerable<Id> GetIdsByLeftId(TLeftId leftId);

        /// <summary>
        /// Return the saved IDs of the entities with the corresponding right
        /// ID. If there are none, then this throws an exception.
        /// </summary>
        IEnumerable<Id> GetIdsByRightId(TRightId rightId);

        /// <summary>
        /// Return the saved relational entity that have the supplied DTO. If
        /// there is no corresponding DTO, an exception is thrown.
        /// </summary>
        /// <remarks>
        /// This will not attempt to find entities of the inverse order of
        /// left/right IDs, if appropriate.
        /// </remarks>
        TEntityRelation GetByDTO(TRelationDTO dto);
        Task<TEntityRelation> GetByDTOAsync(TRelationDTO dto);

        /// <summary>
        /// This will stage everything with the supplied left ID to be deleted.
        /// </summary>
        void DeleteByLeftId(TLeftId id);
        Task DeleteByLeftIdAsync(TLeftId id);

        /// <summary>
        /// This will stage everything with the supplied right ID to be
        /// deleted.
        /// </summary>
        void DeleteByRightId(TRightId id);
        Task DeleteByRightIdAsync(TRightId id);

        /// <summary>
        /// This will delete the entity with the supplied DTO. If none exists,
        /// no exception is thrown.
        /// </summary>
        void DeleteByDTO(TRelationDTO dto);
        Task DeleteByDTOAsync(TRelationDTO dto);
    }
}