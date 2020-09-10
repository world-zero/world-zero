using System;
using System.Collections.Generic;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

// NOTE: The logic for enforcing a name uniqueness is repeated to enforce a
// unique composite "key" of Left/Right IDs in IRAMEntityRelationRepo. Any
// changes that need to be applied to this class are likely needed there as
// well.

namespace WorldZero.Data.Interface.Repository.Entity.RAM
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    /// <remarks>
    /// Caution is adviced when changing the name of an IdNamedEntity as the
    /// name uniqueness assumes that this will not change outside of an Update.
    /// An Update and then a Delete will still have this issue, the Update must
    /// be Saved-ed first.
    /// This class assumes that entities cannot have unset names.
    /// </remarks>
    public abstract class IRAMIdNamedEntityRepo<TEntity>
        : IRAMIdEntityRepo<TEntity>,
          IIdNamedEntityRepo<TEntity>
        where TEntity : IIdNamedEntity
    {
        // Sure these could use IDs instead of the whole entity, but a
        // reference takes up the same amount of space either way. These
        // follow the same style as the parent's saved/staged dictionaries.
        protected Dictionary<Name, TEntity> _savedNames;
        protected Dictionary<Name, TEntity> _stagedNames;

        // As a name staged to be deleted can be recycled, this will lead to
        // a bug as the old name is not deleted anymore, and an exception will
        // be thrown during Save. To resolve this case, and any other weird
        // name recycling cases, a name that is a recycling of a staged
        // name deletion will throw a copy of itself into this set. On save,
        // the deleted name will check to make sure it is not in this set
        // before getting deleted.
        // This is a dict to track the state of the checks that have ran over a
        // particular value. Once the counter hits 2, it should be deleted as
        // both the old and new references have touched it during Save.
        protected Dictionary<Name, int> _recycledNames;

        /// <summary>
        /// If the supplied name is staged as null (staged to be deleted), then
        /// add the name to `_recycledNames`.
        /// </summary>
        /// <returns>True iff there is a name that is recycled.</returns>
        private bool _recycleIfNeeded(Name oldName)
        {
            if (   (this._stagedNames.ContainsKey(oldName))
                && (this._stagedNames[oldName] == null)   )
            {
                this._recycledNames.Add(oldName, 0);
                return true;
            }
            return false;
        }

        public IRAMIdNamedEntityRepo()
            : base()
        {
            this._savedNames  = new Dictionary<Name, TEntity>();
            this._stagedNames = new Dictionary<Name, TEntity>();
            this._recycledNames = new Dictionary<Name, int>();
        }

        public TEntity GetByName(Name name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (!this._savedNames.ContainsKey(name))
                throw new ArgumentException($"There is no stored entity with the name {name.Get}.");
            return (TEntity) this._savedNames[name].DeepCopy();
        }

        // Cases
        // 0. A new entity with an unstaged name is supplied.
        // 1. An entity with a name that is staged but staged for deletion is
        //      supplied.
        // 2. An entity with an already staged/non-null name is supplied.
        // 3. An entity that is already staged is inserted with the same name.
        // 4. See remarks.
        /// <remarks>
        /// This method has undefined behavior if an entity is inserted, has
        /// its name changed, and is then inserted again.
        /// </remarks>
        public override void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // By nature of being an IIdEntity.
            if (entity.IsIdSet())
                throw new ArgumentException("You cannot insert an entity that already has a set ID, meaning it has already been saved.");

            if (   (this._stagedNames.ContainsKey(entity.Name))
                && (this._stagedNames[entity.Name] != null)   )
            {
                throw new ArgumentException($"You cannot have another staged entity of name {entity.Name.Get}.");
            }
            base.Insert(entity);
            this._recycleIfNeeded(entity.Name);
            this._stagedNames[entity.Name] = entity;
        }

        // Cases
        //      These heavily relies on: an ID cannot be changed afer set.
        // 0. An entity with an unset ID is supplied, meaning updating an
        //      un-saved entity.
        // 1. The name of the entity has not been updated.
        // 2. The name of an entity has been updated and the new name is
        //      unsaved and unstaged.
        // 3. The name of an entity has been updated and the new name is staged
        //      as null (staged to be deleted).
        // 4. The name of an entity has been updated but is already saved or
        //      staged (to non-null).
        public override void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!entity.IsIdSet())
                throw new ArgumentException("You cannot update an entity without a valid ID as the ID is assigned on Save, so it cannot be an update if it does not already exist.");

            if (   (this._savedNames.ContainsKey(entity.Name) )
                || (this._stagedNames.ContainsKey(entity.Name)) )
            {
                this._updateHelper(entity);
            }
            else
            {
                Name old = this._findOldName(entity);
                if (old == null)
                    throw new InvalidOperationException("Failed to find the outdated name.");
                this._stagedNames[old] = null;
                this._stagedNames[entity.Name] = entity;
            }

            base.Update(entity);
        }

        /// <summary>
        /// This path is for addressing a name that is saved and/or staged.
        /// </summary>
        private void _updateHelper(TEntity entity)
        {
            if (this._stagedNames.ContainsKey(entity.Name))
            {
                TEntity staged = this._stagedNames[entity.Name];
                if (staged == null)
                // The name is staged to be deleted, so it can be recycled.
                {
                    this._recycleIfNeeded(entity.Name);
                    this._stagedNames[entity.Name] = entity;
                }
                else if (staged.Id != entity.Id)
                // The name is staged and it isn't the same entity.
                {
                    throw new ArgumentException($"Attempting to set an entity with already staged name {staged.Name.Get}.");
                }
                else
                // The name has not been changed, no action is needed.
                { }
            }
        }

        private Name _findOldName(TEntity entity)
        {
            // StagedNames is checked first in order to make sure the name
            // isn't stale.
            foreach (KeyValuePair<Name, TEntity> pair in this._stagedNames)
            {
                Name name = pair.Key;
                TEntity e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return name;
            }
            foreach (KeyValuePair<Name, TEntity> pair in this._savedNames)
            {
                Name name = pair.Key;
                TEntity e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return name;
            }
            return null;
        }

        // Cases
        // 0. The ID corresponds to a staged-only entity.
        //      This cannot happen as IIdNamedEntities have the ID set on Save,
        //      and inserting an entity with a set ID is undefined per the
        //      parent class.
        // 1. An invalid ID is supplied (invalid IDs are discarded during
        //      base.Save).
        // 2. The ID has a saved correspondent with no staged changes.
        // 3. Valid ID, and the name is unchanged.
        // 4. Valid ID, and the name is staged with a new name.
        // 5. Valid ID, and the name is staged with a recycled name.
        public override void Delete(Id id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (!this._saved.ContainsKey(id))
            {
                base.Delete(id);
                return;
            }

            TEntity saved = this._saved[id];
            if (!this._savedNames.ContainsKey(saved.Name))
                throw new InvalidOperationException("A saved entity does not have a saved name.");

            if (!this._staged.ContainsKey(id))
            {
                this._staged[id] = null;
                this._stagedNames[saved.Name] = null;
            }
            else
                this._deleteStagedChanges(id, saved);

            base.Delete(id);
        }

        /// <summary>
        /// This is a helper to Delete.
        /// </summary>
        private void _deleteStagedChanges(Id id, TEntity saved)
        {
            TEntity staged = this._staged[id];
            if (saved.Name == staged.Name)
            {
                this._stagedNames[staged.Name] = null;
            }
            else
            {
                if (!this._recycledNames.ContainsKey(staged.Name))
                {
                    // This assumes that the name change set the original
                    // name to null.
                    this._stagedNames.Remove(staged.Name);
                }
                else
                {
                    this._recycledNames.Remove(staged.Name);
                    this._stagedNames[staged.Name] = null;
                }
            }
        }

        public override void Save()
        {
            base.Save();
            foreach (KeyValuePair<Name, TEntity> pair in this._stagedNames)
            {
                Name name = pair.Key;
                TEntity e = pair.Value;
                if (e != null)
                    throw new InvalidOperationException("An entity was missed during Save.");
                this._savedNames.Remove(name);
                this._stagedNames.Remove(name);
            }

            var c = this._recycledNames.Count;
            if (c != 0)
                throw new InvalidOperationException($"After Save-ing, there are a number un-processed recycled names: {c}.");
        }

        protected override TEntity CommitDelete(Id id)
        {
            TEntity old = base.CommitDelete(id);
            if (old == null)
                return null;
            this._removeOldName(old);
            return old;
        }

        protected override TEntity CommitChange(Id id, TEntity entity)
        {
            var old = base.CommitChange(id, entity);

            if (this._savedNames.ContainsKey(entity.Name))
            {
                if (this._savedNames[entity.Name].Id == id)
                {
                    // Pass, no name change to worry about.
                }
                else
                // A recycled name is being saved.
                {
                    if (!this._recycledNames.ContainsKey(entity.Name))
                        throw new ArgumentException($"Attempting to Save a name that is already used: {entity.Name.Get}");
                }
            }
            this._removeOldName(old, entity.Name);
            this._savedNames[entity.Name] = (TEntity) entity.DeepCopy();
            this._stagedNames.Remove(entity.Name);
            return old;
        }

        private void _removeOldName(TEntity entity, Name newName=null)
        {
            if (entity == null)
            {
                if (newName != null)
                    this._recycleIncrement(newName);
                return;
            }

            if (!this._savedNames.ContainsKey(entity.Name))
                return;

            if (!this._recycleIncrement(entity.Name))
                this._savedNames.Remove(entity.Name);
        }

        private bool _recycleIncrement(Name name)
        {
            if (!this._recycledNames.ContainsKey(name))
                return false;

            this._recycledNames[name]++;
            var c = this._recycledNames[name];
            if (c > 2)
                throw new InvalidOperationException($"Recycled names has a count of {c}, which is greater than 2, which should not occur.");
            else if (c == 2)
                this._recycledNames.Remove(name);
            return true;
        }
    }
}
