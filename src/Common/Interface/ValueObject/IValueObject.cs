using System.Collections.Generic;
using System.Linq;

namespace WorldZero.Common.Interface.ValueObject
{
    /// <summary>
    /// This is a class template for ValueObjects.
    /// 
    /// Once subclasses have implemented GetAtomicValues, this class will
    /// handle: converting instances to a hash code; comparing an IValueObject
    /// to another object via Equals; comparing two IValueObjects via == and
    /// != .
    /// </summary>
    public abstract class IValueObject
    {
        /// <summary>
        /// The result of this method should only reference the immutable
        /// fields of the class - this is used by at least GetHashCode.
        /// </summary>
        protected abstract IEnumerable<object> GetAtomicValues();

        public static bool operator ==(IValueObject left, IValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IValueObject left, IValueObject right)
        {
            return NotEqualOperator(left, right);
        }

        protected static
        bool EqualOperator(IValueObject left, IValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static
        bool NotEqualOperator(IValueObject left, IValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        public override bool Equals(object obj)
        {
            if ( (obj == null) || (obj.GetType() != this.GetType()) )
                return false;

            IValueObject other = (IValueObject) obj;
            IEnumerator<object> thisValues =
                                    this.GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues =
                                    other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (  (ReferenceEquals(thisValues.Current, null))
                    ^ (ReferenceEquals(otherValues.Current, null))  )
                {
                    return false;
                }

                if ( (thisValues.Current != null)
                  && (!thisValues.Current.Equals(otherValues.Current)) )
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                   .Select(x => x != null ? x.GetHashCode() : 0)
                   .Aggregate((x, y) => x ^ y);
        }
    }
}