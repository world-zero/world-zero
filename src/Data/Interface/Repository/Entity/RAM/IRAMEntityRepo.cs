using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Collections;
using System.Collections.Generic;
using System;

namespace WorldZero.Data.Interface.Repository.Entity.RAM
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// As the name suggests, this repo holds the entities in memory, with
    /// absolutely no persistence. The supplied and returned enties are deep
    /// copies to those that are saved and staged.
    /// This repo will also enforce any rules defined by
    /// `IEntity.GetUniqueRules()`.
    /// In an effort to be similar to a database-connecting repo, this is only
    /// going to throw exceptions about conflicts between staged and saved
    /// entities on Save.
    /// </summary>
    /// <remarks>
    /// Caution is adviced when changing the rule of an entity as the
    /// rule uniqueness assumes that this will not change without of an Update.
    /// An Update and then a Delete will still have this issue, the Update must
    /// be Saved-ed first.
    ///
    /// In order to maintain the flexibility that a database repo would offer,
    /// this is pretty inefficient.
    /// </remarks>
    public abstract class IRAMEntityRepo<TEntity, TId, TIdBuiltIn>
        : IEntityRepo<TEntity, TId, TIdBuiltIn>
        where TEntity : IEntity<TId, TIdBuiltIn>
        where TId : ISingleValueObject<TIdBuiltIn>
    {
        /// <summary>
        /// This method will return `TEntity.GetUniqueRules().Count`. This is
        /// intended to be used just by `IRAMENtityRepo.RuleCount`.
        /// </summary>
        /// <returns>
        /// The non-netagive number of rules in the non-generic class that this
        /// repo stored.
        /// </returns>
        /// <remarks>
        /// It is recommended to make a dummy instance and return the result of
        /// `GetUniqueRules().Count`.
        /// </remarks>
        protected abstract int GetRuleCount();

        /// <summary>
        /// This will return the number of unique rules that a non-generic
        /// entity contains.
        /// </summary>
        protected int RuleCount
        {
            get
            {
                if (this._ruleCount < 0)
                {
                    int r = this.GetRuleCount();
                    if (r < 0)
                        throw new NotImplementedException("GetRuleCount() did not return a non-negative value.");
                    this._ruleCount = r;
                }
                return this._ruleCount;
            }
        }
        private int _ruleCount = -1;

        // These will use use the ID of an entity to key that entity. For
        // staged changes, a null entity reference indicates that the entity
        // of that ID should be deleted. As for inserts and updates, there is
        // no real distinction, except inserts will utilize a method that takes
        // the new entity, and return an ID to use as the key; this is
        // necessary in cases of repo-generated IDs. By default, this method
        // will simply return the ID of the supplied entity.
        protected Dictionary<TId, TEntity> _saved;
        protected Dictionary<TId, TEntity> _staged;

        // The following are derived from this: Dictionary<Name, TEntity>
        //      First, instead of Name, we use a W0Set<object>.
        //      Second, we want lists of these since there can be multiple
        //          rules per entity.
        //
        // Sure, we could probbaly get away with just allowing one rule, but if
        // I'm already refactoring this to have any member(s) be a rule, I'm
        // doing this too.
        protected W0List<Dictionary<W0Set<object>, TEntity>> _savedRules;
        protected W0List<Dictionary<W0Set<object>, TEntity>> _stagedRules;
        protected W0List<Dictionary<W0Set<object>, int>> _recycledRules;

        /// <summary>
        /// If the supplied rule is staged as null (staged to be deleted), then
        /// add the rule to `_recycledRules`.
        /// </summary>
        /// <returns>True iff there is a rule that is recycled.</returns>
        private bool _recycleIfNeeded(int listIndex, W0Set<object> oldRule)
        {
            if (this._recycledRules.Count <= listIndex)
                throw new InvalidOperationException("Attempting to dereference a rule list that does not exist.");

            if (   (this._stagedRules[listIndex].ContainsKey(oldRule))
                && (this._stagedRules[listIndex][oldRule] == null) )
            {
                this._recycledRules[listIndex].Add(oldRule, 0);
                return true;
            }

            return false;
        }

        protected virtual TId GenerateId(TEntity entity)
        {
            return entity.Id;
        }

        public IRAMEntityRepo()
        {
            this._saved  = new Dictionary<TId, TEntity>();
            this._savedRules = new W0List<Dictionary<W0Set<object>, TEntity>>();
            for (int i = 0; i < this.RuleCount; i++)
                this._savedRules.Add(new Dictionary<W0Set<object>, TEntity>());

            this.Discard();
        }

        public void Discard()
        {
            this._staged = new Dictionary<TId, TEntity>();
            this._stagedRules = new W0List<Dictionary<W0Set<object>, TEntity>>();
            this._recycledRules = new W0List<Dictionary<W0Set<object>, int>>();

            for (int i = 0; i < this.RuleCount; i++)
            {
                this._stagedRules.Add(new Dictionary<W0Set<object>, TEntity>());
                this._recycledRules.Add(new Dictionary<W0Set<object>, int>());
            }
        }

        /// <summary>
        /// Get all of the saved entities as an enumerable.
        /// </summary>
        /// <returns>The saved entities enumerable.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            foreach (KeyValuePair<TId, TEntity> pair in this._saved)
            {
                TId id = pair.Key;
                TEntity entity = pair.Value;
                if ( (entity.Id != id) || (!entity.IsIdSet()) )
                    throw new InvalidOperationException("A saved entity without an ID has been discovered.");
                yield return (TEntity) entity.Clone();
            }
        }

        /// <remarks>
        /// This will only search the saved entities.
        /// </remarks>
        public virtual TEntity GetById(TId id)
        {
            if (!this._saved.ContainsKey(id))
                throw new ArgumentException("You cannot get an entity with an ID does not exist.");
            return (TEntity) this._saved[id].Clone();
        }

        // Cases
        // I am not writing cases for combos since each rule is handled
        // individually.
        //
        // 0. A new entity with an unstaged rule is supplied.
        // 1. An entity with a rule that is staged but staged for deletion is
        //      supplied.
        // 2. An entity with an already staged/non-null rule is supplied.
        // 3. An entity that is already staged is inserted with the same rule.
        // 4. See remarks.
        /// <remarks>
        /// This method has undefined behavior if an entity is inserted, has
        /// its name changed, and is then inserted again.
        /// 
        /// This method will make sure that an entity with a set ID cannot be
        /// re-staged (for entities that do not have repo-supplied IDs). This
        /// will not detect re-staged rules.
        /// </remarks>
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TId newId = this.GenerateId(entity);
            if (this._staged.ContainsKey(newId))
                throw new ArgumentException("An entity with an already staged ID was attempted to be inserted.");
            this._staged[newId] = entity;

            var rules = entity.GetUniqueRules();
            if (rules.Count != this.RuleCount)
                throw new ArgumentException("this.RuleCount and TEntity.GetUniqueRules().Count do not match - are you doing things with polymorphism?");
            for (int i = 0; i < this.RuleCount; i++)
            {
                var rule = rules[i];
                this._recycleIfNeeded(i, rule);
                this._stagedRules[i][rule] = entity;
            }
        }

        // Cases
        // I am not writing cases for combos since each rule is handled
        // individually.
        //
        // These heavily relies on: an ID cannot be changed afer set.
        //
        // 0. An entity with an unset ID is supplied, meaning updating an
        //      un-saved entity.
        // 1. The rule of the entity has not been updated.
        // 2. The rule of an entity has been updated and the new rule is
        //      unsaved and unstaged.
        // 3. The rule of an entity has been updated and the new rule is staged
        //      as null (staged to be deleted).
        // 4. The rule of an entity has been updated but is already saved or
        //      staged (to non-null).
        /// <summary>
        /// This will update saved entities and update staged entities if
        /// already staged with a set ID. This will not let unsaved entities
        /// that are staged be updated.
        /// </summary>
        /// <remarks>
        /// WARNING: Say there is a concrete entity that does not need an auto-
        /// generated ID. This will *not* catch the case where an instance of
        /// this entity is inserted and saved normally, and then another entity
        /// with the same ID is "inserted" via Update. This will override the
        /// other entity during Save. This is inconsistent with how a DB repo
        /// would work as the SQL to insert vs update are not the same.
        /// </remarks>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (!entity.IsIdSet())
                throw new ArgumentException("An entity cannot be updated if the ID is unset, as stored entities will have a set ID on save.");

            W0List<W0Set<object>> rules = entity.GetUniqueRules();
            if (rules.Count != this.RuleCount)
                throw new InvalidOperationException("this.RuleCount and TEntity.GetUniqueRules().Count do not match.");

            for (int i = 0; i < this.RuleCount; i++)
            {
                var rule = rules[i];
                if (   (this._savedRules[i].ContainsKey(rule)) 
                    || (this._stagedRules[i].ContainsKey(rule)) )
                {
                    this._updateHelper(entity, i, rule);
                }
                else
                {
                    W0Set<object> oldRule = this._findOldRule(entity, i);
                    if (oldRule == null)
                        throw new InvalidOperationException("Failed to find the outdated rule.");
                    this._stagedRules[i][oldRule] = null;
                    this._stagedRules[i][rule] = entity;
                }
            }

            this._staged[entity.Id] = entity;
        }

        /// <summary>
        /// This path is for addressing a rule that is saved and/or staged.
        /// </summary>
        private
        void _updateHelper(TEntity entity, int listIndex, W0Set<object> rule)
        {
            if (this._stagedRules[listIndex].ContainsKey(rule))
            {
                TEntity staged = this._stagedRules[listIndex][rule];
                if (staged == null)
                {
                    this._recycleIfNeeded(listIndex, rule);
                    this._stagedRules[listIndex][rule] = entity;
                }
                else if (staged.Id != entity.Id)
                // The rule is staged but related to a different entity.
                {
                    throw new ArgumentException("Attempting to set an entity with already staged rule.");
                }
                else
                // The rule is not changed, no action required.
                { }
            }
        }

        private W0Set<object> _findOldRule(TEntity entity, int listIndex)
        {
            // Staged is checked first since it will be less stale.
            foreach (KeyValuePair<W0Set<object>, TEntity> pair in
                this._stagedRules[listIndex])
            {
                W0Set<object> rule = pair.Key;
                TEntity e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return rule;
            }
            foreach (KeyValuePair<W0Set<object>, TEntity> pair in
                this._savedRules[listIndex])
            {
                W0Set<object> rule = pair.Key;
                TEntity e = pair.Value;
                if ( (e != null) && (e.Id == entity.Id) )
                    return rule;
            }
            return null;
        }

        // Cases
        // I am not writing cases for combos since each rule is handled
        // individually.
        //
        // 0. The ID corresponds to a staged-only entity.
        //      This cannot happen as IIdNamedEntities have the ID set on Save,
        //      and inserting an entity with a set ID is undefined per the
        //      parent class.
        // 1. An invalid ID is supplied (invalid IDs are discarded during
        //      base.Save).
        // 2. The ID has a saved correspondent with no staged changes.
        // 3. Valid ID, and the rule is unchanged.
        // 4. Valid ID, and the rule is staged with a new rule.
        // 5. Valid ID, and the rule is staged with a recycled rule.
        /// <summary>
        /// This will delete saved entities. If the entity is only staged, the
        /// entity is disregarded (assuming it has an ID type that must be set
        /// on initialization, like `Name`s). If the entity is staged and
        /// saved, the entity will be staged to be deleted.
        /// </summary>
        public virtual void Delete(TId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (this._isStagedOnlyEntity(id))
                return;

            TEntity saved = this._saved[id];
            W0List<W0Set<object>> rules = saved.GetUniqueRules();
            if (rules.Count != this.RuleCount)
                throw new InvalidOperationException("this.RuleCount and TEntity.GetUniqueRules().Count do not match.");

            for (int i = 0; i < this.RuleCount; i++)
            {
                if (!this._savedRules[i].ContainsKey(rules[i]))
                    throw new InvalidOperationException("A saved entity does not have a saved rule.");
            }

            if (!this._staged.ContainsKey(id))
            {
                this._staged[id] = null;
                for (int i = 0; i < this.RuleCount; i++)
                    this._stagedRules[i][ rules[i] ] = null;
            }
            else
                this._deleteSavedStagedEntity(id, saved);

            this._staged[id] = null;
        }

        private bool _isStagedOnlyEntity(TId id)
        {
            if (!this._saved.ContainsKey(id))
            {
                if (this._staged.ContainsKey(id))
                {
                    W0List<W0Set<object>> rules = this._staged[id].GetUniqueRules();
                    for (int i = 0; i < this.RuleCount; i++)
                        this._stagedRules[i][ rules[i] ] = null;
                }
                this._staged[id] = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// This is a helper to Delete.
        /// </summary>
        private void _deleteSavedStagedEntity(TId id, TEntity saved)
        {
            TEntity staged = this._staged[id];
            W0List<W0Set<object>> stagedRules = staged.GetUniqueRules();
            W0List<W0Set<object>> savedRules = saved.GetUniqueRules();

            for (int i = 0; i < this.RuleCount; i++)
            {
                W0Set<object> stagedRule = stagedRules[i];
                if (savedRules[i] == stagedRule)
                    this._stagedRules[i][ stagedRule ] = null;

                else
                {
                    if (this._recycledRules[i].ContainsKey(stagedRule))
                    {
                        this._recycledRules[i].Remove(stagedRule);
                        this._stagedRules[i][stagedRule] = null;

                    }
                    else
                        this._stagedRules[i].Remove(stagedRule);
                }
            }
        }

        /// <summary>
        /// This will save the staged entities. In the case of repo-generated
        /// IDs, the reference will have its ID set then.
        /// </summary>
        /// <remarks>
        /// Yeah this is super inefficient in order to emulate how DB errors
        /// would be thrown. Good thing this is just a dev tool.
        /// </remarks>
        public virtual void Save()
        {
            Dictionary<TId, TEntity> tempSaved;
            W0List<Dictionary<W0Set<object>, TEntity>> tempSavedRules;
            try
            {
                tempSaved = this._cloneSaved(this._saved);
                tempSavedRules = this._cloneSavedRules(this._savedRules);
            }
            catch (ArgumentException)
            { throw new InvalidOperationException("_clone*() received a null."); }
            
            try
            {
                var idMappings = this.CommitStaged();
                foreach (KeyValuePair<TId, TEntity> p in idMappings)
                    p.Value.Id = p.Key;

                return;
            }
            catch (ArgumentException)
            { }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("An invalid cast occurred. Are you doing weird things with Polymorphism? Is Clone() returning the correct type?", e);
            }

            try
            {
                this._saved = this._cloneSaved(tempSaved);
                this._savedRules = this._cloneSavedRules(tempSavedRules);
            }
            catch (ArgumentException)
            { throw new InvalidOperationException("_clone*() received a null."); }
            this.Discard();
            throw new ArgumentException("Saved() could not be completed due to faulty staged data. Discarding the changes.");
        }

        private Dictionary<TId, TEntity> _cloneSaved(Dictionary<TId, TEntity> dict)
        {
            if (dict == null)
                throw new ArgumentException("dict");

            var r = new Dictionary<TId, TEntity>();
            foreach (KeyValuePair<TId, TEntity> pair in dict)
                r.Add(pair.Key, pair.Value);

            return r;
        }

        private W0List<Dictionary<W0Set<object>, TEntity>> _cloneSavedRules(
            W0List<Dictionary<W0Set<object>, TEntity>> listDict
        )
        {
            if (listDict == null)
                throw new ArgumentException("listDict");

            var r = new W0List<Dictionary<W0Set<object>, TEntity>>();
            for (int i = 0; i < listDict.Count; i++)
            {
                var newDict = new Dictionary<W0Set<object>, TEntity>();
                foreach (KeyValuePair<W0Set<object>, TEntity> pair in
                    listDict[i])
                {
                    newDict.Add(pair.Key, pair.Value);
                }
                r.Add(newDict);
            }
            return r;
        }

        /// <summary>
        /// This will return a dict of IDs and the entity that should have the
        /// ID be set to. If there are no IDs to set, then the dict will be
        /// empty (but never null).
        /// </summary>
        protected virtual Dictionary<TId, TEntity> CommitStaged()
        {
            // We use idMappings during CommitSTaged()
            // because we want to make sure everything can commit without an
            // exception being thrown. Otherwise, entities that were saved
            // before the exception and Discard() will still have their IDs
            // set without being actually saved.
            var idMappings = new Dictionary<TId, TEntity>();
            foreach (KeyValuePair<TId, TEntity> pair in this._staged)
            {
                TId id = pair.Key;
                TEntity entity = pair.Value;

                if (entity == null)
                    this.CommitDelete(id);
                else
                {
                    if (!entity.IsIdSet())
                        idMappings.Add(id, entity);
                    this.CommitChange(id, entity);
                }

                this._staged.Remove(id);
            }

            var c = this._staged.Count;
            if (c != 0)
                throw new InvalidOperationException($"After a Save, there should be no staged entities, but there are {c} staged entities remaining.");


            this._cleanUpRules();
            return idMappings;
        }

        /// <summary>
        /// This is a helper to Save(), it makes sure that the rules were saved
        /// correctly.
        /// </summary>
        private void _cleanUpRules()
        {
            for (int i = 0; i < this.RuleCount; i++)
            {
                var dict = this._stagedRules[i];
                foreach (KeyValuePair<W0Set<object>, TEntity> pair in dict)
                {
                    W0Set<object> rule = pair.Key;
                    TEntity e = pair.Value;
                    if (e != null)
                        throw new InvalidOperationException("An entity was missed during Save.");
                    this._savedRules[i].Remove(rule);
                    this._stagedRules[i].Remove(rule);
                }
            }

            for (int i = 0; i < this.RuleCount; i++)
            {
                int c = this._recycledRules[i].Count;
                if (c != 0)
                    throw new InvalidOperationException($"After Save-ing, there are a number un-processed recycled rules in list {i}: {c}.");
            }
        }

        /// <summary>
        /// Delete the entity with the matching ID. If there is no entity to
        /// delete, then it must be an erroneous deletion (trying to delete
        /// something unsaved), so this will return null.
        /// </summary>
        protected virtual TEntity CommitDelete(TId id)
        {
            TEntity oldEntity = null;
            if (this._saved.ContainsKey(id))
            {
                oldEntity = this._saved[id];
                this._saved.Remove(id);
            }
            if (oldEntity != null)
            {
                for (int i = 0; i < this.RuleCount; i++)
                    this._removeRuleIfNeeded(oldEntity, i);
            }
            return oldEntity;
        }

        private void _removeRuleIfNeeded(
            TEntity entity,
            int listIndex,
            W0Set<object> newRule=null
        )
        {
            if (entity == null)
            {
                if (newRule != null)
                    this._recycleIncrement(listIndex, newRule);
                return;
            }

            W0Set<object> oldRule = entity.GetUniqueRules()[listIndex];
            if (!this._savedRules[listIndex].ContainsKey(oldRule))
                return;

            if (!this._recycleIncrement(listIndex, oldRule))
                this._savedRules[listIndex].Remove(oldRule);
        }

        private bool _recycleIncrement(int listIndex, W0Set<object> rule)
        {
            if (!this._recycledRules[listIndex].ContainsKey(rule))
                return false;

            this._recycledRules[listIndex][rule]++;
            var c = this._recycledRules[listIndex][rule];
            if (c > 2)
                throw new InvalidOperationException($"A recycled rule has been fond with a count of {c}, which is greater than 2, which should not occur.");

            else if (c == 2)
                this._recycledRules[listIndex].Remove(rule);

            return true;
        }

        protected virtual TEntity CommitChange(TId id, TEntity entity)
        {
            TEntity old;
            if (this._saved.ContainsKey(id))
                old = this._saved[id];
            else
                old = null;

            // We perform this here instead of earlier during CommitSTaged()
            // because we want to make sure everything can commit without an
            // exception being thrown. Otherwise, entities that were saved
            // before the exception and Discard() will still have their IDs
            // set without being actually saved.
            var entityClone = (TEntity) entity.Clone();
            if (!entityClone.IsIdSet())
                entityClone.Id = id;

            this._saved[id] = entityClone;
            List<W0Set<object>> rules = entity.GetUniqueRules();
            for (int i = 0; i < this.RuleCount; i++)
            {
                W0Set<object> rule = rules[i];
                if (   (this._savedRules[i].ContainsKey(rule))
                    && (this._savedRules[i][rule].Id != id)
                    && (!this._recycledRules[i].ContainsKey(rule))   )
                {
                    throw new ArgumentException($"Attempting to Save a rule that is already used.");
                }

                this._removeRuleIfNeeded(entity, i);
                this._savedRules[i][rule] = entityClone;
                this._stagedRules[i].Remove(rule);
            }
            return old;
        }
    }
}