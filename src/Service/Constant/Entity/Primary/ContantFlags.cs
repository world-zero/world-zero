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
    public class ConstantFlags : IConstantEntities
        <IFlagRepo, IFlag, Name, string>,
        IFlagService
    {
        public static readonly IFlag Duplicate =
            new UnsafeFlag(new Name("Duplicate"));

        public static readonly IFlag Dangerous =
            new UnsafeFlag(new Name("Dangerous"));

        public static readonly IFlag Inappropriate =
            new UnsafeFlag(new Name("Inappropriate"));

        public ConstantFlags(IFlagRepo repo)
            : base(repo)
        { }

        public override IEnumerable<IFlag> GetEntities()
        {
            yield return Duplicate;
            yield return Dangerous;
            yield return Inappropriate;
        }
    }
}