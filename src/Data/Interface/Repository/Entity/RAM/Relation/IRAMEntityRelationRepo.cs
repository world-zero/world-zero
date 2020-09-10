using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.DTO;
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
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
        : IRAMIdEntityRepo<TEntityRelation>,
          IEntityRelationRepo
        <
            TEntityRelation,
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
        where TLeftSVO : ISingleValueObject<TLeftBuiltIn>
        where TRightSVO : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelation
        <
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
    {
        protected Dictionary
        <
            IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
            TEntityRelation
        >
        _savedDtos;

        protected Dictionary
        <
            IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
            TEntityRelation
        >
        _stagedDtos;

        protected Dictionary
        <IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>, int>
        _recycledDtos;

        /// <summary>
        /// If the supplied DTO is staged as null (staged to be deleted), then
        /// add the DTO to `_recycledDtos`.
        /// </summary>
        /// <returns>True iff there is a DTO that is recycled.</returns>
        private bool _recycleIfNeeded(
            IDualDTO<TLeftSVO,TLeftBuiltIn, TRightSVO, TRightBuiltIn> oldDto
        )
        {
            if (   (this._stagedDtos.ContainsKey(oldDto))
                && (this._stagedDtos[oldDto] == null)   )
            {
                this._recycledDtos.Add(oldDto, 0);
                return true;
            }
            return false;
        }

        public IRAMEntityRelationRepo()
            : base()
        {
            this._savedDtos = new Dictionary
            <
                IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                TEntityRelation
            >();
            this._stagedDtos = new Dictionary
            <
                IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                TEntityRelation
            >();
            this._recycledDtos = new Dictionary
            <
                IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                int
            >();
        }

        public IEnumerable<TEntityRelation> GetByLeftId(TLeftSVO id)
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
                yield return (TEntityRelation) er.DeepCopy();
            }
        }

        public IEnumerable<TEntityRelation> GetByRightId(TRightSVO id)
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
                yield return (TEntityRelation) er.DeepCopy();
            }
        }

        public TEntityRelation GetByDTO(
            IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn> dto)
        {
            if (dto == null)
                throw new ArgumentNullException("dto");

            if (!this._savedDtos.ContainsKey(dto))
                throw new ArgumentException("There is no corresponding DTO.");
            return this._savedDtos[dto];
        }

        /// <remarks>
        /// This method has undefined behavior if an entity is inserted, has
        /// its left and/or right ID changed, and is then inserted again.
        /// </remarks>
        public override void Insert(TEntityRelation entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.IsIdSet())
                throw new ArgumentException("You cannot insert an entity that already has a set ID, meaning it has already been saved.");

            var dto = entity.GetDTO();
            if (   (this._stagedDtos.ContainsKey(dto))
                && (this._stagedDtos[dto] != null)   )
            {
                throw new ArgumentException($"You cannot have a repeated staged entity.");
            }
            base.Insert(entity);
            this._recycleIfNeeded(dto);
            this._stagedDtos[dto] = entity;
        }

        public override void Update(TEntityRelation entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!entity.IsIdSet())
                throw new ArgumentException("You cannot update an entity without a valid ID as the ID is assigned on Save, so it cannot be an update if it does not already exist.");

            var dto = entity.GetDTO();
            if (   (this._savedDtos.ContainsKey(dto) )
                || (this._stagedDtos.ContainsKey(dto)) )
            {
                this._updateHelper(entity);
            }
            else
            {
                var old = this._findOldDto(entity);
                if (old == null)
                    throw new InvalidOperationException("Failed to find the outdated name.");
                this._stagedDtos[old] = null;
                this._stagedDtos[dto] = entity;
            }

            base.Update(entity);
        }

        /// <summary>
        /// This path is for addressing a DTO that is saved and/or staged.
        /// </summary>
        private void _updateHelper(TEntityRelation entity)
        {
            var dto = entity.GetDTO();
            if (this._stagedDtos.ContainsKey(dto))
            {
                TEntityRelation staged = this._stagedDtos[dto];
                if (staged == null)
                // The DTO is staged to be deleted, so it can be recycled.
                {
                    this._recycleIfNeeded(dto);
                    this._stagedDtos[dto] = entity;
                }
                else if (staged.Id != entity.Id)
                // The DTO is staged and it isn't the same entity.
                {
                    throw new ArgumentException($"Attempting to set an entity with already staged left/right ID combo.");
                }
                else
                // The name has not been changed, no action is needed.
                { }
            }
        }

        private IDualDTO
        <
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
        _findOldDto(TEntityRelation entity)
        {
            // StagedDtos is checked first in order to make sure the DTO isn't
            // stale.
            foreach (
                KeyValuePair
                <
                    IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                    TEntityRelation
                >
                pair in this._stagedDtos
            )
            {
                var dto = pair.Key;
                var e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return dto;
            }
            foreach (
                KeyValuePair
                <
                    IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                    TEntityRelation
                >
                pair in this._savedDtos
            )
            {
                var dto = pair.Key;
                var e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return dto;
            }
            return null;
        }

        public override void Delete(Id id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (!this._saved.ContainsKey(id))
            {
                base.Delete(id);
                return;
            }

            TEntityRelation saved = this._saved[id];
            var dto = saved.GetDTO();
            if (!this._savedDtos.ContainsKey(dto))
                throw new InvalidOperationException("A saved entity does not have a saved DTO.");

            if (!this._staged.ContainsKey(id))
            {
                this._staged[id] = null;
                this._stagedDtos[dto] = null;
            }
            else
                this._deleteStagedChanges(id, saved);

            base.Delete(id);
        }

        /// <summary>
        /// This is a helper to Delete.
        /// </summary>
        private void _deleteStagedChanges(Id id, TEntityRelation saved)
        {
            var savedDto = saved.GetDTO();
            TEntityRelation staged = this._staged[id];
            var stagedDto = staged.GetDTO();
            if (savedDto == stagedDto)
            {
                this._stagedDtos[stagedDto] = null;
            }
            else
            {
                if (!this._recycledDtos.ContainsKey(stagedDto))
                {
                    // This assumes that the DTO change set the original
                    // DTO to null.
                    this._stagedDtos.Remove(stagedDto);
                }
                else
                {
                    this._recycledDtos.Remove(stagedDto);
                    this._stagedDtos[stagedDto] = null;
                }
            }
        }

        public override void Save()
        {
            base.Save();
            foreach (
                KeyValuePair
                <
                    IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>,
                    TEntityRelation
                >
                pair in this._stagedDtos
            )
            {
                var dto = pair.Key;
                var e = pair.Value;
                if (e != null)
                    throw new InvalidOperationException("An entity was missed during Save.");
                this._savedDtos.Remove(dto);
                this._stagedDtos.Remove(dto);
            }

            var c = this._recycledDtos.Count;
            if (c != 0)
                throw new InvalidOperationException($"After Save-ing, there are a number un-processed recycled DTOs: {c}.");
        }

        protected override TEntityRelation CommitDelete(Id id)
        {
            TEntityRelation old = base.CommitDelete(id);
            if (old == null)
                return null;
            this._removeOldDto(old);
            return old;
        }

        protected override TEntityRelation CommitChange(
            Id id,
            TEntityRelation entity
        )
        {
            var dto = entity.GetDTO();
            var old = base.CommitChange(id, entity);

            if (this._savedDtos.ContainsKey(dto))
            {
                if (this._savedDtos[dto].Id == id)
                {
                    // Pass, no name change to worry about.
                }
                else
                // A recycled DTO is being saved.
                {
                    if (!this._recycledDtos.ContainsKey(dto))
                        throw new ArgumentException($"Attempting to Save a DTO that is already used: id {entity.Id}.");
                }
            }
            this._removeOldDto(old, dto);
            this._savedDtos[dto] = (TEntityRelation) entity.DeepCopy();
            this._stagedDtos.Remove(dto);
            return old;
        }

        private void _removeOldDto(
            TEntityRelation entity,
            IDualDTO<TLeftSVO,TLeftBuiltIn,TRightSVO,TRightBuiltIn> newDto=null
        )
        {
            if (entity == null)
            {
                if (newDto != null)
                    this._recycleIncrement(newDto);
                return;
            }

            var dto = entity.GetDTO();
            if (!this._savedDtos.ContainsKey(dto))
                return;

            if (!this._recycleIncrement(dto))
                this._savedDtos.Remove(dto);
        }

        private bool _recycleIncrement(
            IDualDTO<TLeftSVO,TLeftBuiltIn,TRightSVO,TRightBuiltIn> dto
        )
        {
            if (!this._recycledDtos.ContainsKey(dto))
                return false;

            this._recycledDtos[dto]++;
            var c = this._recycledDtos[dto];
            if (c > 2)
                throw new InvalidOperationException($"Recycled names has a count of {c}, which is greater than 2, which should not occur.");
            else if (c == 2)
                this._recycledDtos.Remove(dto);
            return true;
        }
    }
}