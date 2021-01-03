using System;
using System.Collections.Generic;

namespace WorldZero.Common.Collections.Generic
{
    /// <summary>
    /// W0List<T> is a subclass of List<T>. This class makes the addition of
    /// overriding `Equals(object)` and `GetHashCode()`.
    /// </summary>
    /// <remarks>
    /// The time complexity of both of these operations is O(n).
    /// </remarks>
    public class W0List<T> : List<T>
    {
        public W0List()
            : base()
        { }

        public W0List(List<T> list)
            : base()
        {
            if (list == null)
                throw new ArgumentNullException("list");

            foreach (T t in list)
                this.Add(t);
        }

        public override int GetHashCode()
        {
            int r = 1;
            foreach (T t in this)
                r *= t.GetHashCode();
            return r;
        }

        public override bool Equals(object obj)
        {
            if ( (obj == null) || (obj.GetType() != this.GetType()) )
                return false;

            var other = (W0List<T>) obj;
            if (this.Count != other.Count)
                return false;

            var thisValues = this.GetEnumerator();
            var otherValues = other.GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (  (ReferenceEquals(thisValues.Current, null))
                    ^ (ReferenceEquals(otherValues.Current, null))  )
                {
                    return false;
                }

                if (   (thisValues.Current != null)
                    && (!thisValues.Current.Equals(otherValues.Current)))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }
    }
}