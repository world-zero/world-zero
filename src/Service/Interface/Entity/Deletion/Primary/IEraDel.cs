using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <summary>
    /// The `EraDel` will not allow itself to be instantiated or extended;
    /// this is because the process of rolling an era over is handled by
    /// `EraReg`.
    /// </summary>
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    public interface IEraDel
        : IEntityDel<IEra, Name, string>
    { }
}