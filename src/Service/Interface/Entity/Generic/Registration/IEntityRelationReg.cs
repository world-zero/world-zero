using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn"/>
    /// <summary>
    /// This is a generic interface for entity relation creation service
    /// classes.
    /// </summary>
    /// <typeparam name="TEntityRelation">
    /// The IEntityRelation implementation that this class abstracts.
    /// </typeparam>
    /// <typeparam name="TLeftEntity">
    /// This is the entity that maps to `TEntityRelation`'s `LeftId`.
    /// </typeparam>
    /// <typeparam name="TLeftId">
    /// This is the `ISingleValueObject` implementation that `TEntityRelation`
    /// uses as a left ID.
    /// </typeparam>
    /// <typeparam name="TLeftBuiltIn">
    /// This is the built-in type behind `TLeftId`.
    /// </typeparam>
    /// <typeparam name="TRightEntity">
    /// This is the entity that maps to `TEntityRelation`'s `RightId`.
    /// </typeparam>
    /// <typeparam name="TRightId">
    /// This is the `ISingleValueObject` implementation that `TEntityRelation`
    /// uses as a right ID.
    /// </typeparam>
    /// <typeparam name="TRightBuiltIn">
    /// This is the built-in type behind `TRightId`.
    /// </typeparam>
    /// <typeparam name="TRelationDTO">
    /// This is needed for the `TEntityRelationRepo`, but this class does not
    /// actually care about this type outside of that.
    /// </typeparam>
    /// <remarks>
    /// For ease of use, it is recommended to have a property similar to the
    /// below to easily and consistently cast up in children. This example is
    /// taken from CreateEra.
    /// <code>
    /// protected IEraRepo _eraRepo { get { return (IEraRepo) this._relRepo; } }
    /// </code>
    /// It is also worth noting that we don't actually care about the DTO type
    /// for the registration class itself - this is why there is no
    /// `IEntityRelationCntReg`.
    /// </remarks>
    public interface IEntityRelationReg
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IEntityReg<TEntityRelation, Id, int>
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : class, IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightEntity : class, IEntity<TRightId, TRightBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    { }
}