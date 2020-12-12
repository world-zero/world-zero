using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset"/>
    public class StatusDel
        : ABCEntityDel<UnsafeStatus, Name, string>
    {
        protected readonly PraxisDel _praxisDel;
        protected readonly TaskDel _taskDel;
        protected readonly MetaTaskUnset _mtUnset;

        public StatusDel(
            IStatusRepo repo,
            PraxisDel praxisDel,
            TaskDel taskDel,
            MetaTaskUnset mtUnset
        )
            : base(repo)
        {
            this.AssertNotNull(praxisDel, "praxisDel");
            this.AssertNotNull(taskDel, "taskDel");
            this.AssertNotNull(mtUnset, "mtUnset");

            this._praxisDel = praxisDel;
            this._taskDel = taskDel;
            this._mtUnset = mtUnset;
        }

        public override void Delete(Name statusId)
        {
            void f(Name status)
            {
                this._taskDel.DeleteByStatus(status);
                this._praxisDel.DeleteByStatus(status);
                this._mtUnset.DeleteByStatus(status);
                base.Delete(status);
            }

            this.Transaction<Name>(f, statusId, true);
        }
    }
}