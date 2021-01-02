using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IMetaTaskTagReg
        : IEntityRelationReg
        <
            IMetaTaskTag,
            IMetaTask,
            Id,
            int,
            ITag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    { }
}