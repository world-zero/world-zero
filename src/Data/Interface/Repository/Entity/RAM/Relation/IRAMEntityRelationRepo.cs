using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;

// NOTE: The logic for enforcing unique composite "key" of Left/Right IDs is
// repeated to enforce a name uniqueness in IAMIdNamedEntityRepo. Any changes
// that need to be applied to this class are likely needed there as well.

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Relation
{
    public abstract class IRAMEntityRelationRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IRAMIdEntityRepo<TEntityRelation>,
          IEntityRelationRepo
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
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        public IRAMEntityRelationRepo()
            : base()
        { }

        public IEnumerable<TEntityRelation> GetByLeftId(TLeftId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            IEnumerable<TEntityRelation> ers = 
                from er in this._saved.Values
                where er.LeftId == id
                select er;

            if (ers.Count() == 0)
                throw new ArgumentException($"There are no LeftIds with the value {id.Get}.");

            foreach (TEntityRelation er in ers)
            {
                if (!er.IsIdSet())
                    throw new InvalidOperationException("A saved entity without an ID has been discovered.");
                yield return (TEntityRelation) er.Clone();
            }
        }

        public IEnumerable<TEntityRelation> GetByRightId(TRightId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            IEnumerable<TEntityRelation> ers = 
                from er in this._saved.Values
                where er.RightId == id
                select er;

            if (ers.Count() == 0)
                throw new ArgumentException($"There are no RightIds with the value {id.Get}.");

            foreach (TEntityRelation er in ers)
            {
                if (!er.IsIdSet())
                    throw new InvalidOperationException("A saved entity without an ID has been discovered.");
                yield return (TEntityRelation) er.Clone();
            }
        }

        public TEntityRelation GetByDTO(TRelationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("dto");

            IEnumerable<TEntityRelation> match = 
                from e in this._saved.Values
                where e.GetDTO().Equals(dto)
                select e;

            var c = match.Count();
            if (c == 1)
                return (TEntityRelation) match.First().Clone();
            else if (c == 0)
                throw new ArgumentException($"Could not find an entity with the supplied DTO.");
            else
                throw new InvalidOperationException($"Multiple DTOs found, which is a bug.");
        }
    }
}