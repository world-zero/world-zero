using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

// NOTE: This class is used in testing `IIdStatusedEntiyDel`.

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// As you migh expect, deleting a praxis will delete the Comments, Tags,
    /// Flags, and Participants (and Votes by extension) on that praxis.
    /// </summary>
    /// <remarks>
    /// This will also delete the participant's received votes. Whether or not
    /// the voting character will receive a refund is up to <see
    /// cref="VoteDel"/>.
    /// <br />
    /// Dev note: shockingly, deleting a praxis has no special logic outside of
    /// cascading the deletion.
    /// </remarks>
    public class PraxisDel : IIdStatusedEntityDel<UnsafePraxis>
    {
        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._repo; } }

        protected readonly PraxisParticipantDel _ppDel;
        protected readonly CommentDel _commentDel;
        protected readonly PraxisTagDel _praxisTagDel;
        protected readonly PraxisFlagDel _praxisFlagDel;

        public PraxisDel(
            IUnsafePraxisRepo praxisRepo,
            PraxisParticipantDel ppDel,
            CommentDel commentDel,
            PraxisTagDel praxisTagDel,
            PraxisFlagDel praxisFlagDel
        )
            : base(praxisRepo)
        {
            this.AssertNotNull(ppDel, "ppDel");
            this.AssertNotNull(commentDel, "commentDel");
            this.AssertNotNull(praxisTagDel, "praxisTagDel");
            this.AssertNotNull(praxisFlagDel, "praxisFlagDel");

            this._ppDel = ppDel;
            this._commentDel = commentDel;
            this._praxisTagDel = praxisTagDel;
            this._praxisFlagDel = praxisFlagDel;
        }

        public override void Delete(Id praxisId)
        {
            void op(Id praxisId0)
            {
                this._commentDel.DeleteByPraxis(praxisId0);
                this._praxisTagDel.DeleteByPraxis(praxisId0);
                this._praxisFlagDel.DeleteByPraxis(praxisId0);
                base.Delete(praxisId0);
                this._ppDel.UNSAFE_DeleteByPraxis(praxisId0);
            }

            this.Transaction<Id>(op, praxisId);
        }

        public void DeleteByTask(Id taskId)
        {
            void op(Id id)
            {
                IEnumerable<UnsafePraxis> praxises;
                try
                {
                    praxises = this._praxisRepo.GetByTaskId(taskId);
                    foreach (UnsafePraxis p in praxises)
                        this.Delete(p);
                }
                catch (ArgumentException)
                { return; }
            }

            this.Transaction<Id>(op, taskId, true);
        }

        public void DeleteByTask(UnsafeTask task)
        {
            this.AssertNotNull(task, "task");
            this.DeleteByTask(task.Id);
        }

        public async System.Threading.Tasks.Task DeleteByTaskasync(UnsafeTask task)
        {
            this.AssertNotNull(task, "task");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByTask(task));
        }

        public async System.Threading.Tasks.Task DeleteByTaskasync(Id taskId)
        {
            this.AssertNotNull(taskId, "taskId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByTask(taskId));
        }
    }
}