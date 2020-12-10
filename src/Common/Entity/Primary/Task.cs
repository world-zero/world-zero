using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ITask"/>
    public sealed class Task
        : IUnsafeIdStatusedProxy<UnsafeTask>, ITask
    {
        public Task(UnsafeTask task)
            : base(task)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Task(this._unsafeEntity);
        }

        public string Summary => this._unsafeEntity.Summary;
        public Name FactionId => this._unsafeEntity.FactionId;
        public PointTotal Points => this._unsafeEntity.Points;
        public Level Level => this._unsafeEntity.Level;
        public Level MinLevel => this._unsafeEntity.MinLevel;
        public bool IsHistorianable => this._unsafeEntity.IsHistorianable;
    }
}