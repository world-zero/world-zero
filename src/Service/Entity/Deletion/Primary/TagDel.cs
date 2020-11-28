using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    public class TagDel : IEntityDel<Tag, Name, string>
    {
        protected readonly TaskTagDel _taskTagDel;
        protected readonly MetaTaskTagDel _mtTagDel;
        protected readonly PraxisTagDel _praxisTagDel;

        public TagDel(
            ITagRepo tagRepo,
            TaskTagDel taskTagDel,
            MetaTaskTagDel mtTagDel,
            PraxisTagDel praxisTagDel
        )
            : base(tagRepo)
        {
            this.AssertNotNull(mtTagDel, "mtTagDel");
            this.AssertNotNull(taskTagDel, "taskTagDel");
            this.AssertNotNull(praxisTagDel, "praxisTagDel");
            this._taskTagDel = taskTagDel;
            this._mtTagDel = mtTagDel;
            this._praxisTagDel = praxisTagDel;
        }

        public override void Delete(Name tagId)
        {
            void op(Name tagName)
            {
                this._taskTagDel.DeleteByTag(tagName);
                this._mtTagDel.DeleteByTag(tagName);
                this._praxisTagDel.DeleteByTag(tagName);
                base.Delete(tagName);
            }

            this.Transaction<Name>(op, tagId);
        }
    }
}