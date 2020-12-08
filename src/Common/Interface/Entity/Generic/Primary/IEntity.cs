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
    /// </remarks>
    public interface IEntity<TSingleValObj, TValObj>
        where TSingleValObj : ISingleValueObject<TValObj>
    {
        /// <summary>
        /// This is the Id for an entity - it is a value object with a single
        /// (or primary) value. This cannot be changed after being set.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown if a set Id is attempted to be changed.
        /// </exception>
        TSingleValObj Id { get; }

        bool IsIdSet();

        IEntity<TSingleValObj, TValObj> Clone();
    }
}