using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    public class FlagDel : IEntityDel<Flag, Name, string>
    {
        protected readonly TaskFlagDel _taskFlagDel;
        protected readonly MetaTaskFlagDel _mtFlagDel;
        protected readonly PraxisFlagDel _praxisFlagDel;

        public FlagDel(
            IFlagRepo flagRepo,
            TaskFlagDel taskFlagDel,
            MetaTaskFlagDel mtFlagDel,
            PraxisFlagDel praxisFlagDel
        )
            : base(flagRepo)
        {
            this.AssertNotNull(mtFlagDel, "mtFlagDel");
            this.AssertNotNull(taskFlagDel, "taskFlagDel");
            this.AssertNotNull(praxisFlagDel, "praxisFlagDel");
            this._taskFlagDel = taskFlagDel;
            this._mtFlagDel = mtFlagDel;
            this._praxisFlagDel = praxisFlagDel;
        }

        public override void Delete(Name flagId)
        {
            void op(Name flagName)
            {
                this._taskFlagDel.DeleteByFlag(flagName);
                this._mtFlagDel.DeleteByFlag(flagName);
                this._praxisFlagDel.DeleteByFlag(flagName);
                base.Delete(flagName);
            }

            this.Transaction<Name>(op, flagId);
        }
    }
}