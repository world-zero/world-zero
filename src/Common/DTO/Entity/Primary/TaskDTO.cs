using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="ITaskDTO"/>
    public class TaskDTO : IdStatusedDTO, ITaskDTO
    {
        public string Summary { get; }
        public Name FactionId { get; }
        public PointTotal Points { get; }
        public Level Level { get; }
        public Level MinLevel { get; }
        public bool IsHistorianable { get; }

        public TaskDTO(
            Id id=null,
            Name status=null,
            string summary=null,
            Name factionId=null,
            PointTotal points=null,
            Level level=null,
            Level minLevel=null,
            bool isHistorianable=false
        )
            : base(id, status)
        {
            this.Summary = summary;
            this.FactionId = factionId;
            this.Points = points;
            this.Level = level;
            this.MinLevel = minLevel;
            this.IsHistorianable = isHistorianable;
        }

        public override object Clone()
        {
            return new TaskDTO(
                this.Id,
                this.StatusId,
                this.Summary,
                this.FactionId,
                this.Points,
                this.Level,
                this.MinLevel,
                this.IsHistorianable
            );
        }

        public override bool Equals(IDTO dto)
        {
            var t = dto as TaskDTO;
            return
                t != null
                && base.Equals(t)
                && this.XOR(this.Summary, t.Summary)
                && this.XOR(this.FactionId, t.FactionId)
                && this.XOR(this.Points, t.Points)
                && this.XOR(this.Level, t.Level)
                && this.XOR(this.MinLevel, t.MinLevel)
                && this.XOR(this.IsHistorianable, t.IsHistorianable);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.Summary != null) r *= this.Summary.GetHashCode();
                if (this.FactionId != null) r *= this.FactionId.GetHashCode();
                if (this.Points != null) r *= this.Points.GetHashCode();
                if (this.Level != null) r *= this.Level.GetHashCode();
                if (this.MinLevel != null) r *= this.MinLevel.GetHashCode();
                return r;
            }
        }
    }
}