using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Deletion
{
    /// <summary>
    /// The `EraDel` will not allow itself to be instantiated; this is because
    /// the process of rolling an era over is handled by `EraReg`.
    /// </summary>
    public sealed class EraDel : IEntityDel<Era, Name, string>
    {
        public EraDel(IEraRepo eraRepo)
            : base(eraRepo)
        {
            throw new NotImplementedException("EraDel cannot be instantiated; use EraReg instead.");
        }
    }
}