using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Primary;

namespace WorldZero.Service.Constant.Entity.Primary
{
    /// <inheritdoc cref="IConstantEntities"/>
    public class ConstantStatuses : IConstantEntities
        <IStatusRepo, IStatus, Name, string>,
        IStatusService
    {
        public static readonly IStatus Proposed =
            new UnsafeStatus(new Name("Proposed"));

        public static readonly IStatus Active =
            new UnsafeStatus(new Name("Active"));

        public static readonly IStatus InProgress =
            new UnsafeStatus(new Name("In Progress"));

        public static readonly IStatus Retired =
            new UnsafeStatus(new Name("Retired"));

        public ConstantStatuses(IStatusRepo repo)
            : base(repo)
        { }

        public override IEnumerable<IStatus> GetEntities()
        {
            yield return Proposed;
            yield return Active;
            yield return InProgress;
            yield return Retired;
        }
    }
}