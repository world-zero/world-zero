using System;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Primary;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEraDel"/>
    public sealed class EraDel : ABCEntityDel<IEra, Name, string>, IEraDel
    {
        public EraDel(IEraRepo eraRepo)
            : base(eraRepo)
        {
            throw new NotImplementedException("EraDel cannot be instantiated; use EraReg instead.");
        }
    }
}