using System;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <summary>
    /// This is the interface for an Entity, complete with an Id.
    /// </summary>
    /// <remarks>
    /// Unless a reference-type property states that it can store null, assume
    /// that these properties will throw an `ArgumentNullException` when
    /// appropriate.
    /// <br />
    /// These interfaces tend to be just getters for a reason:
    /// compiler-enforced safety against updating entities without the use of a
    /// service updating class. This is relevant as the system-wide logic is
    /// enforced there. For more, see <see
    /// cref="WorldZero.Common.Interface.Entity.Marker.IUnsafeEntity"/>.
    /// </remarks>
    public interface IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        /// <summary>
        /// This is the Id for an entity - it is a value object with a single
        /// (or primary) value. This cannot be changed after being set.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown if a set Id is attempted to be changed.
        /// </exception>
        TId Id { get; }

        bool IsIdSet();

        IEntity<TId, TBuiltIn> Clone();
    }
}