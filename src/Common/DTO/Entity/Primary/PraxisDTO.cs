using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IPraxisDTO"/>
    public class PraxisDTO : IdStatusedDTO, IPraxisDTO
    {
        public Id TaskId { get; }
        public PointTotal Points { get; }
        public Id MetaTaskId { get; }
        public bool AreDueling { get; }

        public PraxisDTO(
            Id id=null,
            Name statusId=null,
            Id taskId=null,
            PointTotal points=null,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base(id, statusId)
        {
            this.TaskId = taskId;
            this.Points = points;
            this.MetaTaskId = metaTaskId;
            this.AreDueling = areDueling;
        }

        public override object Clone()
        {
            return new PraxisDTO(
                this.Id,
                this.StatusId,
                this.TaskId,
                this.Points,
                this.MetaTaskId,
                this.AreDueling
            );
        }

        public override bool Equals(IDTO dto)
        {
            var p = dto as PraxisDTO;
            return
                p != null
                && base.Equals(p)
                && this.XOR(this.TaskId, p.TaskId)
                && this.XOR(this.Points, p.Points)
                && this.XOR(this.MetaTaskId, p.MetaTaskId)
                && this.XOR(this.AreDueling, p.AreDueling);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.TaskId != null) r *= this.TaskId.GetHashCode();
                if (this.Points != null) r *= this.Points.GetHashCode();
                if (this.MetaTaskId != null) r *= this.MetaTaskId.GetHashCode();
                return r;
            }
        }
    }
}