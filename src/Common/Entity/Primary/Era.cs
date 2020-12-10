using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IEra"/>
    public sealed class Era : IUnsafeNamedProxy<UnsafeEra>, IEra
    {
        public Era(UnsafeEra era)
            : base(era)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Era(this._unsafeEntity);
        }

        public PastDate StartDate => this._unsafeEntity.StartDate;
        public PastDate EndDate => this._unsafeEntity.EndDate;
        public Level TaskLevelBuffer => this._unsafeEntity.TaskLevelBuffer;
        public int MaxPraxises => this._unsafeEntity.MaxPraxises;
        public int MaxTasks => this._unsafeEntity.MaxTasks;
        public int MaxTasksReiterator => this._unsafeEntity.MaxTasksReiterator;
    }
}