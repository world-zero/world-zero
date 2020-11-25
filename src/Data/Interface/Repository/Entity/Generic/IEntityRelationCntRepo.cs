using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Generic
{
    /// <summary>
    /// This repo specializes in relational entities who can have several
    /// LeftId+RightId combinations, tracking each submission with a Count
    /// member in the entity.
    /// </summary>
    public interface IEntityRelationCntRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IEntityRelationRepo
          <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
          >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelationCnt
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : CntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        /// <summary>
        /// Get a collection of entities who have the same left and right IDs
        /// as the supplied DTO, ignoring the Count member. Just like GetByDTO,
        /// this will only search the saved entities; similarly, an exception
        /// is thrown if there are no matches.
        /// </summary>
        IEnumerable<TEntityRelation> GetByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );

        /// <summary>
        /// This will stage everything with the supplied partial DTO to be
        /// deleted.
        /// </summary>
        void DeleteByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );
        Task DeleteByPartialDTOAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );

        /// <summary>
        /// Return an int that is the next largest Count for the supplied DTO.
        /// </summary>
        /// <remarks>
        /// This will not attempt to "fill in" any deleted ints, but just
        /// determine the int of the largest int plus one, where the largest
        /// int is taken from the the saved pool and the staged pool. This
        /// would mean that this would "fill in" an int of something that used
        /// to have the largest int, until it was deleted, freeing that slot
        /// up.
        /// </remarks>
        int GetNextCount(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );
        Task<int> GetNextCountAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        );
    }
}