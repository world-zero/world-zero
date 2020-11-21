using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.RAM.Entity.Relation
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
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
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
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
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
                from eTemp in this._saved.Values
                let e = this.TEntityCast(eTemp)
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

        public async Task<TEntityRelation> GetByDTOAsync(TRelationDTO dto)
        {
            return this.GetByDTO(dto);
        }

        public void DeleteByLeftId(TLeftId id)
        {
            // Yes, it would be more effecient to check staged first, but this
            // way will reveal bugs more easily.
            if (id == null)
                throw new ArgumentNullException("id");

            IEnumerable<TEntityRelation> ers = 
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
                where er.LeftId != null
                where er.LeftId == id
                select er;

            foreach (TEntityRelation er in ers)
                this.Delete(er.Id);

            // It is possible to delete a staged-only entity, and there are
            // some entity types that do not have an ID set until Save(), so it
            // is necessary to check the entity with context for the ID.
            var ids = new List<Id>();
            foreach (KeyValuePair<object, object> p in this._staged)
            {
                Id tempId;
                TEntityRelation er;
                try
                {
                    tempId = this.TIdCast(p.Key);
                    er = this.TEntityCast(p.Value);
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException("A cast failed.", e); }

                if ( (er != null) && (er.LeftId != null) && (er.LeftId == id) )
                    ids.Add(tempId);
            }

            foreach (Id tempId in ids)
                this.Delete(tempId);
        }

        public async Task DeleteByLeftIdAsync(TLeftId id)
        {
            this.DeleteByLeftId(id);
        }

        public void DeleteByRightId(TRightId id)
        {
            // Yes, it would be more effecient to check staged first, but this
            // way will reveal bugs more easily.
            if (id == null)
                throw new ArgumentNullException("id");

            IEnumerable<TEntityRelation> ers = 
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
                where er.RightId != null
                where er.RightId == id
                select er;

            foreach (TEntityRelation er in ers)
                this.Delete(er.Id);

            // It is possible to delete a staged-only entity, and there are
            // some entity types that do not have an ID set until Save(), so it
            // is necessary to check the entity with context for the ID.
            var ids = new List<Id>();
            foreach (KeyValuePair<object, object> p in this._staged)
            {
                Id tempId;
                TEntityRelation er;
                try
                {
                    tempId = this.TIdCast(p.Key);
                    er = this.TEntityCast(p.Value);
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException("A cast failed.", e); }

                if ( (er != null) && (er.RightId != null) && (er.RightId == id) )
                    ids.Add(tempId);
            }

            foreach (Id tempId in ids)
                this.Delete(tempId);
        }

        public async Task DeleteByRightIdAsync(TRightId id)
        {
            this.DeleteByRightId(id);
        }

        public void DeleteByDTO(TRelationDTO dto)
        {
            // Since this is the full DTO, there can only be a max of one saved
            // and one staged result.

            if (dto == null)
                throw new ArgumentNullException("dto");

            IEnumerable<TEntityRelation> match = 
                from eTemp in this._saved.Values
                let e = this.TEntityCast(eTemp)
                where e.GetDTO().Equals(dto)
                select e;

            var c = match.Count();
            if (c == 1)
            {
                this.Delete(match.First().Id);
            }
            else if (c > 1)
                throw new InvalidOperationException($"Multiple DTOs found, which is a bug.");

            // It is possible to delete a staged-only entity, and there are
            // some entity types that do not have an ID set until Save(), so it
            // is necessary to check the entity with context for the ID.
            foreach (KeyValuePair<object, object> p in this._staged)
            {
                Id tempId;
                TEntityRelation er;
                try
                {
                    tempId = this.TIdCast(p.Key);
                    er = this.TEntityCast(p.Value);
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException("A cast failed.", e); }

                if (er != null)
                {
                    if (er.GetDTO().Equals(dto))
                        this.Delete(tempId);
                        return;
                }
            }
        }

        public async Task DeleteByDTOAsync(TRelationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("dto");

            await Task.Run(() => this.DeleteByDTO(dto));
        }

        internal override TEntityRelation TEntityCast(object o)
        {
            try
            {
                return (TEntityRelation) o;
            }
            catch (InvalidCastException e)
            { throw new InvalidOperationException("There is an incompatible type stored as a TEntityRelation.", e); }
        }
    }
}