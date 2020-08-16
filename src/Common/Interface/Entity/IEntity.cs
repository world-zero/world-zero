using System.Runtime.CompilerServices;
using System;

[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]

namespace WorldZero.Common.Interface.Entity
{
    /// <summary>
    /// This is the interface for an Entity with an Id.
    /// </summary>
    public abstract class IEntity<SingleValObj, VOType>
        where SingleValObj : ISingleValueObject<VOType>
    {
        public bool IsIdSet()
        {
            if (this._id == null)
                return false;
            return !this._id.Equals(this.UnsetIdValue);
        }

        abstract public IEntity<SingleValObj, VOType> DeepCopy();

        /// <summary>
        /// This is the Id for an entity - it is a value object with a single
        /// (or primary) value. This cannot be changed after being set.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown if a set Id is attempted to be changed.
        /// </exception>
        public SingleValObj Id
        {
            get { return this._id; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Attempted to set an `Id` to null.");

                if (this.IsIdSet())
                    throw new ArgumentException("Attempted to re-set the `Id` of an entity.");

                this._id = value;
            }
        }
        private SingleValObj _id;

        // This is functionally a readonly member, but my IDE was having none
        // of that, so here we are.
        /// <summary>
        /// An ID with this value is considered unset, and can still be
        /// changed.
        /// </summary>
        public SingleValObj UnsetIdValue { get; private set; }

        public IEntity(SingleValObj unsetValue)
        {
            this.UnsetIdValue = unsetValue;
            this._id = this.UnsetIdValue;
        }
    }
}