using System.Runtime.CompilerServices;
using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

// This is necessary for the Dapper-compatible constructors, which will need to
// be utilized in Data.
[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("WorldZero.Test.Unit")]
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    public abstract class ABCEntity<TId, TBuiltIn>
        : IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public bool IsIdSet()
        {
            if (this._id == null)
                return false;
            return !this._id.Equals(this.UnsetIdValue);
        }

        public virtual W0List<W0Set<object>> GetUniqueRules()
        {
            var r = new W0List<W0Set<object>>();
            return r;
        }

        abstract public IEntity<TId, TBuiltIn> CloneAsEntity();

        public object Clone()
        {
            return (object) this.CloneAsEntity();
        }

        public TId Id
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
        private TId _id;

        /// <summary>
        /// An ID with this value is considered unset, and can still be
        /// changed.
        /// </summary>
        protected readonly TId UnsetIdValue;

        public ABCEntity(TId unsetValue)
        {
            this.UnsetIdValue = unsetValue;
            this._id = this.UnsetIdValue;
        }
    }
}