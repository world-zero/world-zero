using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IMetaTaskReg
        : IEntityReg<IMetaTask, Id, int>
    { }
}