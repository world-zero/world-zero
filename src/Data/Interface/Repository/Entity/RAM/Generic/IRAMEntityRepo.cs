using System.Threading.Tasks;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Collections.Generic;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]

// It's not a terrible idea to refactor this and have Staged track the CRUD
// type (create, update, delete) associated with the staged change.

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <summary>
    /// This is the needed data that a specific entity repo would require to
    /// work as a RAM entity repo.
    /// </summary>
    /// <remarks>
    /// This is just going to make sure that nothing it holds is null or
    /// negative, where appropriate.
    /// <br />
    /// Inserting `null` as a value in `Saved` is unhandled.
    /// <br />
    /// Each instance contains a reference to a repo that uses that reference.
    /// This allows a collection of `EntityData's to be controllable via their
    /// repos, which is extremely helpful during repo transaction methods.
    /// </remarks>
    public class EntityData
    {
        public readonly int RuleCount;
        /// <summary>
        /// This is a reference to the RAM repository that stores this
        /// structure.
        /// </summary>
        public readonly IStaticRAMEntityRepo Repo;

        public EntityData(int ruleCount, IStaticRAMEntityRepo repo)
        {
            if (ruleCount < 0)
                throw new ArgumentException("ruleCount cannot be negative.");
            if (repo == null)
                throw new ArgumentNullException("repo");
            this.RuleCount = ruleCount;
            this.Repo = repo;
            this.Clean();
        }

        /// <summary>
        /// This will clean an instance to the default state, containing no
        /// state data outside of the rule count.
        /// </summary>
        /// <remarks>
        /// This will not change TxnDepth.
        /// </remarks>
        public void Clean()
        {
            this.Saved = new Dictionary<object, object>();
            this.SavedRules = new W0List<Dictionary<W0Set<object>, object>>();
            for (int i = 0; i < this.RuleCount; i++)
                this.SavedRules.Add(new Dictionary<W0Set<object>, object>());

            this.CleanStaged();
        }

        /// <summary>
        /// This will clean out an instance's staged and recycled data.
        /// </summary>
        /// <remarks>
        /// This will not change TxnDepth.
        /// </remarks>
        public void CleanStaged()
        {
            this.Staged = new Dictionary<object, object>();

            this.StagedRules = new W0List<Dictionary<W0Set<object>, object>>();
            for (int i = 0; i < this.RuleCount; i++)
                this.StagedRules.Add(new Dictionary<W0Set<object>, object>());

            this.RecycledRules = new W0List<Dictionary<W0Set<object>, int>>();
            for (int i = 0; i < this.RuleCount; i++)
                this.RecycledRules.Add(new Dictionary<W0Set<object>, int>());
        }

        /// <summary>
        /// Return a deep copy of an instance of `EntityData`.
        /// </summary>
        /// <remarks>
        /// It's worth noting that this performs shallow copies of the saved
        /// data. This is acceptable since IRAMEntityRepo uses deep copies to
        /// store/return entities, so the only reference to saved data is
        /// stored being cloned here, and is not changed anywhere else. That
        /// said, this logic does not apply to Staged data, but since this is
        /// intended only to be used as a means of rolling back failed
        /// saves/transactions, we don't care about the staged data since it's
        /// going to be deleted anyways.
        /// </remarks>
        public EntityData Clone()
        {
            var clone = new EntityData(this.RuleCount, this.Repo);

            foreach (KeyValuePair<object, object> p in this.Saved)
                clone.Saved.Add(p.Key, p.Value);

            foreach (KeyValuePair<object, object> p in this.Staged)
                clone.Staged.Add(p.Key, p.Value);

            for (int i = 0; i < this.RuleCount; i++)
            {
                foreach (KeyValuePair<W0Set<object>, object> p in
                    this.SavedRules[i])
                {
                    clone.SavedRules[i].Add(p.Key, p.Value);
                }

                foreach (KeyValuePair<W0Set<object>, object> p in
                    this.StagedRules[i])
                {
                    clone.StagedRules[i].Add(p.Key, p.Value);
                }

                foreach (KeyValuePair<W0Set<object>, int> p in
                    this.RecycledRules[i])
                {
                    clone.RecycledRules[i].Add(p.Key, p.Value);
                }
            }

            return clone;
        }

        /// <summary>
        /// Dictionary<TId, TEntity>
        /// </summary>
        public Dictionary<object, object> Saved
        {
            get { return this._saved; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Saved");
                this._saved = value;
            }
        }
        private Dictionary<object, object> _saved;

        /// <summary>
        /// Dictionary<TId, TEntity>
        /// </summary>
        public Dictionary<object, object> Staged
        {
            get { return this._staged; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Staged");
                this._staged = value;
            }
        }
        private Dictionary<object, object> _staged;

        /// <summary>
        /// W0List<Dictionary<W0Set<object>, TEntity>>
        /// </summary>
        public W0List<Dictionary<W0Set<object>, object>> SavedRules
        {
            get { return this._savedRules; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SavedRules");
                this._savedRules = value;
            }
        }
        private W0List<Dictionary<W0Set<object>, object>> _savedRules;

        /// <summary>
        /// W0List<Dictionary<W0Set<object>, TEntity>>
        /// </summary>
        public W0List<Dictionary<W0Set<object>, object>> StagedRules
        {
            get { return this._stagedRules; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("StagedRules");
                this._stagedRules = value;
            }
        }
        private W0List<Dictionary<W0Set<object>, object>> _stagedRules;
        
        /// <summary>
        /// W0List<Dictionary<W0Set<object>, int>>
        /// </summary>
        public W0List<Dictionary<W0Set<object>, int>> RecycledRules
        {
            get { return this._recycledRules; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("RecycledRules");
                this._recycledRules = value;
            }
        }
        private W0List<Dictionary<W0Set<object>, int>> _recycledRules;
    }

    /// <summary>
    /// This is a helper base class used to get around some static/typing
    /// issues that exist when "implementing" `IEntityRepo` directly with
    /// `IRAMEntityRepo`.
    /// </summary>
    /// <remarks>
    /// Since generic classes of different types aren't actually the same
    /// class, it is necessary to put the static member that should be shared
    /// regardless of types as an abstract base class.
    /// </remarks>
    public abstract class IStaticRAMEntityRepo
    {
        // This exists here because a repo has a variety of types, which make
        // casting to it dynamically extremely unpleasant. This way, we can
        // cast down to this parent UnsafeI and have polymorphism save us from all
        // that nasty typing and reflection headache.
        public abstract void Save();

        /// <summary>
        /// This maps a concrete IEntity.GetType().Name to an instance of
        /// EntityData.
        /// </summary>
        /// <summary>
        /// This allows all of the data to be shared between the different
        /// repos, allowing us to build transactions.
        /// </summary>
        protected static Dictionary<string, EntityData> _data =
            new Dictionary<string, EntityData>();

        /// <summary>
        /// This is where _data is backed up during a transaction to ensure
        /// that the entire transaction can be rolled back at any time. This is
        /// `null` when there is no transaction occurring.
        /// </summary>
        protected static Dictionary<string, EntityData> _tempData = null;

        protected static int _txnDepth
        {
            get { return _txnDepthPrivate; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("TxnDepth cannot be negative.");
                _txnDepthPrivate = value;
            }
        }
        private static int _txnDepthPrivate;

        /// <summary>
        /// If not present, create a new `name` : `EntityData` pair in the
        /// static data; this will throw an ArgumentException if the
        /// `ruleCount` is invalid for `EntityData`.
        /// </summary>
        /// <remarks>
        /// This is used instead of a constructor as `this.GetType().Name` in
        /// children classes does not work when being passed to a base
        /// constructor as `this` does not exist in that context.
        /// </remarks>
        protected void InitIfNeeded(string name, int ruleCount)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (!_data.ContainsKey(name))
                _data[name] = new EntityData(ruleCount, this);
        }
    }

    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// As the name suggests, this repo holds the entities in static memory,
    /// with absolutely no persistence.
    /// </summary>
    /// <remarks>
    /// THIS DEV-TOOL IS NOT MULTI-THREAD SAFE. Be advised that implementations
    /// that return the same `GetEntityName()` will operate on the same data.
    /// <br/>
    /// The supplied and returned entities are deep copies to those that are
    /// saved and staged.
    /// <br/>
    /// This repo will enforce any rules defined by `IEntity.GetUniqueRules()`.
    /// <br/>
    /// In an effort to be similar to a database-connecting repo, this is only
    /// going to throw exceptions about conflicts between staged and saved
    /// entities on Save.
    /// <br/>
    /// Caution is adviced when changing the rule of an entity as the
    /// rule uniqueness assumes that this will not change without of an Update.
    /// An Update and then a Delete will still have this issue, the Update must
    /// be Saved-ed first.
    /// <br/>
    /// In order to maintain the flexibility that a database repo would offer,
    /// this is pretty inefficient.
    /// </remarks>
    public abstract class IRAMEntityRepo<TEntity, TId, TIdBuiltIn>
        : IStaticRAMEntityRepo,
          IEntityRepo<TEntity, TId, TIdBuiltIn>
        where TEntity : UnsafeIEntity<TId, TIdBuiltIn>
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
                        throw new InvalidOperationException("GetRuleCount() did not return a non-negative value.");
                    this._ruleCount = r;
                }
                return this._ruleCount;
            }
        }
        private int _ruleCount = -1;

        protected readonly string _entityName;

        // These will use use the ID of an entity to key that entity. For
        // staged changes, a null entity reference indicates that the entity
        // of that ID should be deleted. As for inserts and updates, there is
        // no real distinction, except inserts will utilize a method that takes
        // the new entity, and return an ID to use as the key; this is
        // necessary in cases of repo-generated IDs. By default, this method
        // will simply return the ID of the supplied entity.
        protected Dictionary<object, object> _saved
        {
            get { return _data[this._entityName].Saved; }
            set
            {
                try
                {
                    _data[this._entityName].Saved = value;
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException(e.Message); }
            }
        }
        protected Dictionary<object, object> _staged
        {
            get { return _data[this._entityName].Staged; }
            set
            {
                try
                {
                    _data[this._entityName].Staged = value;
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException(e.Message); }
            }
        }

        // The following are derived from this: Dictionary<Name, TEntity>
        //      First, instead of Name, we use a W0Set<object>.
        //      Second, we want lists of these since there can be multiple
        //          rules per entity.
        //
        // Sure, we could probbaly get away with just allowing one rule, but if
        // I'm already refactoring this to have any member(s) be a rule, I'm
        // doing this too.
        protected W0List<Dictionary<W0Set<object>, object>> _savedRules
        {
            get { return _data[this._entityName].SavedRules; }
            set
            {
                try
                {
                    _data[this._entityName].SavedRules = value;
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException(e.Message); }
            }
        }
        protected W0List<Dictionary<W0Set<object>, object>> _stagedRules
        {
            get { return _data[this._entityName].StagedRules; }
            set
            {
                try
                {
                    _data[this._entityName].StagedRules = value;
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException(e.Message); }
            }
        }
        protected W0List<Dictionary<W0Set<object>, int>> _recycledRules
        {
            get { return _data[this._entityName].RecycledRules; }
            set
            {
                try
                {
                    _data[this._entityName].RecycledRules = value;
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException(e.Message); }
            }
        }

        /// <summary>
        /// Cast and return the argument to `TId`, otherwise throwing an
        /// `InvalidOperationException`.
        /// </summary>
        internal TId TIdCast(object o)
        {
            try
            {
                return (TId) o;
            }
            catch (InvalidCastException e)
            { throw new InvalidOperationException("There is an incompatible type stored as a TId.", e); }
        }

        /// <summary>
        /// Cast and return the argument to `TEntity`, otherwise throwing an
        /// `InvalidOperationException`.
        /// </summary>
        internal virtual TEntity TEntityCast(object o)
        {
            try
            {
                return (TEntity) o;
            }
            catch (InvalidCastException e)
            { throw new InvalidOperationException("There is an incompatible type stored as a TEntity.", e); }
        }

        /// <summary>
        /// If the supplied rule is staged as null (staged to be deleted), then
        /// add the rule to `_recycledRules`.
        /// </summary>
        /// <returns>True iff there is a rule that is recycled.</returns>
        /// <remarks>
        /// `className` is specified if it is desired to check for a repo that
        /// isn't the current instance.
        /// </remarks>
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

        /// <remarks>
        /// This will create the concrete class' EntityData instance, if it
        /// does not already exist.
        /// </remarks>
        public IRAMEntityRepo()
        {
            this._entityName = typeof(TEntity).FullName;
            try
            {
                base.InitIfNeeded(this._entityName, this.RuleCount);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException(e.Message, e); }
        }

        public void Clean()
        {
            _data[this._entityName].Clean();
        }

        public async Task CleanAsync()
        {
            this.Clean();
        }

        public void CleanAll()
        {
            foreach (KeyValuePair<string, EntityData> p in _data)
                p.Value.Clean();
        }

        public async Task CleanAllAsync()
        {
            this.CleanAll();
        }

        public void Discard()
        {
            _data[this._entityName].CleanStaged();
        }

        public async Task DiscardAsync()
        {
            this.Discard();
        }

        /// <summary>
        /// Get all of the saved entities as an enumerable.
        /// </summary>
        /// <returns>The saved entities enumerable.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            foreach (KeyValuePair<object, object> pair in this._saved)
            {
                TId id = this.TIdCast(pair.Key);
                TEntity entity = this.TEntityCast(pair.Value);

                if ( (entity.Id != id) || (!entity.IsIdSet()) )
                    throw new InvalidOperationException("A saved entity without an ID has been discovered.");

                yield return (TEntity) entity.Clone();
            }
        }

        /// <remarks>
        /// This will only search the saved entities.
        /// </remarks>
        public TEntity GetById(TId id)
        {
            if (!this._saved.ContainsKey(id))
                throw new ArgumentException("You cannot get an entity with an ID does not exist.");
            return (TEntity) this.TEntityCast(this._saved[id]).Clone();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return this.GetById(id);
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
        /// This method will make sure that an entity with a set ID cannot be
        /// re-staged (for entities that do not have repo-supplied IDs). This
        /// will not detect re-staged rules.
        /// <br />
        /// BUG: An entity with an already present ID (that doesn't have
        /// repo-assigned IDs) that is inserted will not be caught, it will
        /// just override the saved entiy.
        /// </remarks>
        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TId newId = this.GenerateId(entity);
            if (this._staged.ContainsKey(newId))
            {
                if (this._staged[newId] !=  null)
                    throw new ArgumentException("An entity with an already staged ID was attempted to be inserted.");
            }
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

        public async Task InsertAsync(TEntity entity)
        {
            this.Insert(entity);
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
        public void Update(TEntity entity)
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

        public async Task UpdateAsync(TEntity entity)
        {
            this.Update(entity);
        }

        /// <summary>
        /// This path is for addressing a rule that is saved and/or staged.
        /// </summary>
        private
        void _updateHelper(TEntity entity, int listIndex, W0Set<object> rule)
        {
            if (this._stagedRules[listIndex].ContainsKey(rule))
            {
                TEntity staged =
                    this.TEntityCast(this._stagedRules[listIndex][rule]);
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
            foreach (KeyValuePair<W0Set<object>, object> pair in
                this._stagedRules[listIndex])
            {
                W0Set<object> rule = pair.Key;
                TEntity e = this.TEntityCast(pair.Value);
                if ( (e != null) && (e.Id == entity.Id) )
                    return rule;
            }
            foreach (KeyValuePair<W0Set<object>, object> pair in
                this._savedRules[listIndex])
            {
                W0Set<object> rule = pair.Key;
                TEntity e = this.TEntityCast(pair.Value);
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

            TEntity saved = this.TEntityCast(this._saved[id]);
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

        public async Task DeleteAsync(TId id)
        {
            this.Delete(id);
        }

        private bool _isStagedOnlyEntity(TId id)
        {
            if (!this._saved.ContainsKey(id))
            {
                if (this._staged.ContainsKey(id))
                {
                    W0List<W0Set<object>> rules =
                        this.TEntityCast(this._staged[id]).GetUniqueRules();
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
            TEntity staged = this.TEntityCast(this._staged[id]);
            if (this._staged[id] == null)
                return;
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
        /// In short, this will serve as a means for subclasses to easily
        /// verify the contents of saved before the save is truly committed. A
        /// subclass that wants to cancel a save due to invalid data should
        /// throw an ArgumentException during this method. On Save failure,
        /// this is ran again to assert that the newly restored saved states
        /// are still valid, tripping up an InvalidOperationException if they
        /// are not.
        /// </summary>
        /// <remarks>
        /// This will be performed just after staged has been moved into saved,
        /// and before entities have their IDs saved to outside references (if
        /// necessary), as well as being after the staged states have been
        /// cleaned out. We can get away with being this inefficient since this
        /// is just a dev tool.
        /// </remarks>
        protected virtual void FinalChecks()
        { }

        /// <summary>
        /// This will save the staged entities. In the case of repo-generated
        /// IDs, the reference will have its ID set then.
        /// </summary>
        /// <remarks>
        /// Yeah this is super inefficient in order to emulate how DB errors
        /// would be thrown. Good thing this is just a dev tool.
        /// </remarks>
        public override void Save()
        {
            if (   (this._staged.Count == 0)
                && (this._stagedRules.Count == 0)
                && (this._recycledRules.Count == 0)   )
            {
                // There's no work to be done, don't bother.
                return;
            }

            EntityData backupED = _data[this._entityName].Clone();
            backupED.CleanStaged();

            try
            {
                var idMappings = this.CommitStaged();
                // No errors occurred during save! Now we can safely apply the
                // side effect changes to entity references (if appropriate)
                // and return.
                foreach (KeyValuePair<object, object> p in idMappings)
                    this.TEntityCast(p.Value).Id = this.TIdCast(p.Key);

                return;
            }
            catch (ArgumentException e)
            {
                this._restoreSavedStates(backupED, e);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("An invalid cast occurred. Are you doing weird things with Polymorphism? Is Clone() returning the correct type?", e);
            }
        }

        public async Task SaveAsync()
        {
            this.Save();
        }

        /// <summary>
        /// A save could not be committed, roll back to the previous saved
        /// states with a clean staged states. This will re-run FinalChecks()
        /// to ensure that the restored state is valid as well.
        /// </summary>
        private void _restoreSavedStates(EntityData backupED, Exception exc)
        {
            _data[this._entityName] = backupED;
            this.Discard();
            try
            { this.FinalChecks(); }
            catch (ArgumentException e)
            {
                throw new InvalidOperationException($"After restoring Saved, the data of repo {this._entityName} is still corrupt.", e);
            }
            throw new ArgumentException("Save() could not be completed due to faulty staged data. Discarding the changes.", exc);
        }

        /// <summary>
        /// This will return a dict of IDs and the entity that should have the
        /// ID be set to. If there are no IDs to set, then the dict will be
        /// empty (but never null).
        /// </summary>
        protected virtual
        Dictionary<object, object> CommitStaged()
        {
            // We use idMappings during CommitStaged()
            // because we want to make sure everything can commit without an
            // exception being thrown. Otherwise, entities that were saved
            // before the exception and Discard() will still have their IDs
            // set without being actually saved.
            var idMappings = new Dictionary<object, object>();
            foreach (KeyValuePair<object, object> pair in this._staged)
            {
                TId id = this.TIdCast(pair.Key);
                TEntity entity = this.TEntityCast(pair.Value);

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
                throw new InvalidOperationException($"After a Save, there should be no staged entities, but there are {c} staged entities remaining in {this._entityName}.");

            this._cleanUpRules();
            this.FinalChecks();
            return idMappings;
        }

        /// <summary>
        /// This is a helper to Save(), it makes sure that the rules were saved
        /// correctly.
        /// </summary>
        private void _cleanUpRules()
        {
            for (int i = 0; i < this._ruleCount; i++)
            {
                var dict = this._stagedRules[i];
                foreach (KeyValuePair<W0Set<object>, object> pair in dict)
                {
                    W0Set<object> rule = pair.Key;
                    TEntity e = this.TEntityCast(pair.Value);
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
                oldEntity = this.TEntityCast(this._saved[id]);
                this._saved.Remove(id);
            }
            if (oldEntity != null)
            {
                for (int i = 0; i < this._ruleCount; i++)
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
                old = this.TEntityCast(this._saved[id]);
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
            for (int i = 0; i < this._ruleCount; i++)
            {
                W0Set<object> rule = rules[i];
                if (   (this._savedRules[i].ContainsKey(rule))
                    && (this.TEntityCast(this._savedRules[i][rule]).Id != id)
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

        public void BeginTransaction(bool serializeLock=false)
        {
            if (_txnDepth++ == 0)
            {
                _tempData = new Dictionary<string, EntityData>();
                foreach (KeyValuePair<string, EntityData> p in _data)
                    _tempData[p.Key] = p.Value.Clone();
            }
        }

        public async Task BeginTransactionAsync(bool serializeLock=false)
        {
            this.BeginTransaction(serializeLock);
        }

        /// <remarks>
        /// There will be artifacts from repo-generated IDs where this fails on
        /// a subsequent save.
        /// </remarks>
        public void EndTransaction()
        {
            if (!this.IsTransactionActive())
                return;

            if (--_txnDepth != 0)
                return;

            var tempData = _tempData;
            _tempData = null;
            try
            {
                foreach (EntityData ed in _data.Values)
                    ed.Repo.Save();
            }
            catch (ArgumentException e)
            {
                _tempData = tempData;
                this.DiscardTransaction();
                throw new ArgumentException("There was an error Saving a transaction, the whole transaction has been discarded.", e);
            }
        }

        public async Task EndTransactionAsync()
        {
            this.EndTransaction();
        }

        public void DiscardTransaction()
        {
            if (!this.IsTransactionActive())
                return;

            // We don't actually need to make a deep copy since we're just
            // scrubbing out the old version anyways.
            _data = _tempData;
            _tempData = null;
            _txnDepth = 0;
        }

        public async Task DiscardTransactionAsync()
        {
            this.DiscardTransaction();
        }

        public bool IsTransactionActive()
        {
            if (_tempData == null)
                return false;
            return true;
        }

        public async Task<bool> IsTransactionActiveAsync()
        {
            return this.IsTransactionActive();
        }
    }
}