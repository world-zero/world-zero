using System.Runtime.CompilerServices;
using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.General.Generic;

// This is necessary for the Dapper-compatible constructors, which will need to
// be utilized in Data.
[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]

namespace WorldZero.Common.Interface.Entity
{
    /// <summary>
    /// This is the interface for an Entity, complete with an Id.
    /// </summary>
    public abstract class IEntity<TSingleValObj, TValObj>
        where TSingleValObj : ISingleValueObject<TValObj>
    {
        public bool IsIdSet()
        {
            if (this._id == null)
                return false;
            return !this._id.Equals(this.UnsetIdValue);
        }

        /// <summary>
        /// This method will return a list of sets, each of which contains
        /// at least one member that a repository should ensure are unique as a
        /// combiniation, per set. This does not include the Id of an entity.
        /// </summary>
        /// <returns>
        /// A list of HashSets of ISingleValueObjects and/or built in types
        /// that repos must consider treat as unique for a specific instance.
        /// These types will be able to cast to object and have .Equals work
        /// appropriately. This will never return null, but it can return an
        /// empty list.
        /// </returns>
        internal virtual W0List<W0Set<object>> GetUniqueRules()
        {
            var r = new W0List<W0Set<object>>();
            return r;
        }

        abstract public IEntity<TSingleValObj, TValObj> Clone();

        /// <summary>
        /// This is the Id for an entity - it is a value object with a single
        /// (or primary) value. This cannot be changed after being set.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown if a set Id is attempted to be changed.
        /// </exception>
        public TSingleValObj Id
        {
            get { return this._id; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Id");

                if (this.IsIdSet())
                    throw new ArgumentException("Attempted to re-set the `Id` of an entity.");

                this._id = value;
            }
        }
        private TSingleValObj _id;

        // This is functionally a readonly member, but my IDE was having none
        // of that, so here we are.
        /// <summary>
        /// An ID with this value is considered unset, and can still be
        /// changed.
        /// </summary>
        public TSingleValObj UnsetIdValue { get; private set; }

        public IEntity(TSingleValObj unsetValue)
        {
            this.UnsetIdValue = unsetValue;
            this._id = this.UnsetIdValue;
        }
    }
}