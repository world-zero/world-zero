using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// As you migh expect, deleting a praxis will delete the Comments, Votes,
    /// Tags, Flags, and Participants on that praxis. The points used for the
    /// votes will not be refunded.
    /// </summary>
    /// <remarks>
    /// Dev note: shockingly, deleting a praxis has no special logic outside of
    /// cascading the deletion.
    /// </remarks>
    public class PraxisDel : IEntityDel<Praxis, Id, int>
    {
        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._repo; } }

        protected readonly IPraxisParticipantRepo _ppRepo;
        protected readonly CommentDel _commentDel;
        protected readonly VoteDel _voteDel;
        protected readonly PraxisTagDel _praxisTagDel;
        protected readonly PraxisFlagDel _praxisFlagDel;

        public PraxisDel(
            IPraxisRepo praxisRepo,
            IPraxisParticipantRepo ppRepo,
            CommentDel commentDel,
            VoteDel voteDel,
            PraxisTagDel praxisTagDel,
            PraxisFlagDel praxisFlagDel
        )
            : base(praxisRepo)
        {
            this.AssertNotNull(ppRepo, "ppRepo");
            this.AssertNotNull(commentDel, "commentDel");
            this.AssertNotNull(voteDel, "voteDel");
            this.AssertNotNull(praxisTagDel, "praxisTagDel");
            this.AssertNotNull(praxisFlagDel, "praxisFlagDel");

            this._ppRepo = ppRepo;
            this._commentDel = commentDel;
            this._voteDel = voteDel;
            this._praxisTagDel = praxisTagDel;
            this._praxisFlagDel = praxisFlagDel;
        }

        public override void Delete(Id praxisId)
        {
            void op(Id praxisId0)
            {
                this._ppRepo.DeleteByPraxisId(praxisId0);
                this._commentDel.DeleteByPraxis(praxisId0);
                this._voteDel.DeleteByPraxis(praxisId0);
                this._praxisTagDel.DeleteByPraxis(praxisId);
                this._praxisFlagDel.DeleteByPraxis(praxisId);
                base.Delete(praxisId0);
            }

            this.Transaction<Id>(op, praxisId);
        }

        public void DeleteByTask(Id taskId)
        {
            void op(Id id)
            {
                IEnumerable<Praxis> praxises;
                try
                { praxises = this._praxisRepo.GetByTaskId(taskId); }
                catch (ArgumentException)
                { return; }

                foreach (Praxis p in praxises)
                    this.Delete(p);
            }

            this.Transaction<Id>(op, taskId, true);
        }

        public void DeleteByTask(Task task)
        {
            this.AssertNotNull(task, "task");
            this.DeleteByTask(task.Id);
        }

        public async System.Threading.Tasks.Task DeleteByTaskasync(Task task)
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