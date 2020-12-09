using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <summary>
    /// This entity represents a many-to-many mapping between two entities by
    /// containing their IDs. Equality is determined by the keys of the two
    /// entities. If these keys are the same type, then either order will be
    /// considered the same. If the keys are of a different type, then order
    /// will be considered, meaning a child with an `Id` LeftId and a `Name`
    /// RightId will not be considered equal to a child with a `Name` LeftId
    /// and an `Id` RightId, even if the corresponding properties have the same
    /// values.
    /// </summary>
    /// <remarks>
    /// Additionally, validatation of IDs falls to the repository and service
    /// class(es), as necessary.
    /// <br />
    /// As usual, enforcing that the combination of the left and right is the
    /// responsiblity of the repo.  
    /// <br />
    /// When making an implementation, please have the class name match the
    /// order of the enitites (ie: an implementation that maps left entity
    /// `Praxis` and right entity `Flag` should be named something like
    /// `PraxisFlag`). An exception to this rule is for self-referencial
    /// relations.
    /// <br />
    /// It is worth noting that == and != should not be overloaded, just
    /// .Equals().
    /// <br />
    /// It is recommended that children add more descriptive wrapper properties
    /// for LeftId and RightId.
    /// </remarks>
    public interface IEntityRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IIdEntity
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        TLeftId LeftId { get; }
        TRightId RightId { get; }
        RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> GetDTO();
    }
}