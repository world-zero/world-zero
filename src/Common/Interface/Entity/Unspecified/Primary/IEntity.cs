using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    /// <summary>
    /// This is the interface for an Entity, complete with an Id.
    /// </summary>
    /// <remarks>
    /// Unless a reference-type property states that it can store null, assume
    /// that these properties will throw an `ArgumentNullException` when
    /// appropriate.
    /// <br />
    /// Clones are deep copies.
    /// <br />
    /// These interfaces tend to be just getters for a reason:
    /// compiler-enforced safety against updating entities without the use of a
    /// service updating class. This is relevant as the system-wide logic is
    /// enforced there. Additionally, in order to ensure that entities are not
    /// being created erroneously, the non-generic concrete entities will exist
    /// within the corresponding non-generic IEntityService class. This will
    /// allow for the searching and creation entity service classes to act as
    /// factories, circumventing the vulnerability of having an entity be
    /// created to be almost identical to another entity but with an "update"
    /// applied via any changed properties.
    /// </remarks>
    public interface IEntity<TId, TBuiltIn> : ICloneable
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        /// <summary>
        /// This is the Id for an entity - it is a value object with a single
        /// (or primary) value. This cannot be changed after being set.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown if a set Id is attempted to be changed.
        /// </exception>
        TId Id { get; set; }

        bool IsIdSet();

        // TODO: remove this; see notes for more
        /// <summary>
        /// Similar to <see cref="System.ICloneable.Clone"/>, except this will
        /// return the result as an entity.
        /// </summary>
        IEntity<TId, TBuiltIn> CloneAsEntity();

        /// <summary>
        /// Unless you are implementing a whole new entity or you are working
        /// on the RAMEntityRepos, you can safely ignore this method.
        /// <br />
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
        W0List<W0Set<object>> GetUniqueRules();
    }
}