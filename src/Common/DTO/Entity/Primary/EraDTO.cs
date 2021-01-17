using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="IEraDTO"/>
    public class EraDTO : EntityDTO<Name, string>, IEraDTO
    {
        public PastDate StartDate { get; }
        public PastDate EndDate { get; }
        public Level TaskLevelBuffer { get; }
        public int MaxPraxises { get; }
        public int MaxTaskCompletion { get; }
        public int MaxTaskCompletionReiterator { get; }

        public EraDTO(
            Name id=null,
            PastDate startDate=null,
            PastDate endDate=null,
            Level taskLevelBuffer=null,
            int maxPraxises=20,
            int maxTaskCompletion=1,
            int maxTaskCompletionReiterator=2
        )
            : base(id)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TaskLevelBuffer = taskLevelBuffer;
            this.MaxPraxises = maxPraxises;
            this.MaxTaskCompletion = maxTaskCompletion;
            this.MaxTaskCompletionReiterator = maxTaskCompletionReiterator;
        }

        public override object Clone()
        {
            return new EraDTO(
                this.Id,
                this.StartDate,
                this.EndDate,
                this.TaskLevelBuffer,
                this.MaxPraxises,
                this.MaxTaskCompletion,
                this.MaxTaskCompletionReiterator
            );
        }

        public override bool Equals(IDTO dto)
        {
            var e = dto as EraDTO;
            return
                e != null
                && base.Equals(e)
                && this.XOR(e.StartDate, this.StartDate)
                && this.XOR(e.EndDate, this.EndDate)
                && this.XOR(e.TaskLevelBuffer, this.TaskLevelBuffer)
                && this.XOR(e.MaxPraxises, this.MaxPraxises)
                && this.XOR(e.MaxTaskCompletion, this.MaxTaskCompletion)
                && this.XOR(e.MaxTaskCompletionReiterator, this.MaxTaskCompletionReiterator);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.StartDate != null) r *= this.StartDate.GetHashCode();
                if (this.EndDate != null) r *= this.EndDate.GetHashCode();
                if (this.TaskLevelBuffer != null) r *= this.TaskLevelBuffer.GetHashCode();
                r *= this.MaxPraxises;
                r *= this.MaxTaskCompletion;
                r *= this.MaxTaskCompletionReiterator;
                return r;
            }
        }
    }
}