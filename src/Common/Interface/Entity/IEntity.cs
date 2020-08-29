using System.Runtime.CompilerServices;
using System;

[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]

namespace WorldZero.Common.Interface.Entity
{
    /// <summary>
    /// This is the interface for an Entity with an Id.
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

        abstract public IEntity<TSingleValObj, TValObj> DeepCopy();

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