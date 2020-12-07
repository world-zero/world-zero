using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// The `EraDel` will not allow itself to be instantiated; this is because
    /// the process of rolling an era over is handled by `EraReg`.
    /// </summary>
    public sealed class EraDel : IEntityDel<UnsafeEra, Name, string>
    {
        public EraDel(IUnsafeEraRepo eraRepo)
            : base(eraRepo)
        {
            throw new NotImplementedException("EraDel cannot be instantiated; use EraReg instead.");
        }
    }
}