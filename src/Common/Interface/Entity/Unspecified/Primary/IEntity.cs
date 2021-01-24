using System;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

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
    /// Clones are deep copies of the underlying DTO.
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
    public interface IEntity<TId, TBuiltIn> :
        IEntityDTO<TId, TBuiltIn>,
        ICloneable
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        bool IsIdSet();
    }
}