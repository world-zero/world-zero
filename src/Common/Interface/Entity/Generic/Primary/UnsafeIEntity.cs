using System.Runtime.CompilerServices;
using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Marker;

// This is necessary for the Dapper-compatible constructors, which will need to
// be utilized in Data.
[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class UnsafeIEntity<TSingleValObj, TValObj>
        : IEntity<TSingleValObj, TValObj>, IUnsafeEntity
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

        /// <summary>
        /// An ID with this value is considered unset, and can still be
        /// changed.
        /// </summary>
        protected readonly TSingleValObj UnsetIdValue;

        public UnsafeIEntity(TSingleValObj unsetValue)
        {
            this.UnsetIdValue = unsetValue;
            this._id = this.UnsetIdValue;
        }
    }
}