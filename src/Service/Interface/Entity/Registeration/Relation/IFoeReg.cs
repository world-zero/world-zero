using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Relation
{
    /// <remarks>
    /// This will not allow friends to be foes.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IFoeReg
        : IEntityRelationReg
        <
            IFoe,
            ICharacter,
            Id,
            int,
            ICharacter,
            Id,
            int,
            NoIdRelationDTO<Id, int, Id, int>
        >
    { }
}