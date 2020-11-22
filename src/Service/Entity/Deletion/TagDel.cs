using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Service.Interface.Entity.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion
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

        public override void Delete(Name tagName)
        {
            this.AssertNotNull(tagName, "tagName");
            this.BeginTransaction();

            this._taskTagDel.DeleteByTag(tagName);
            this._mtTagDel.DeleteByTag(tagName);
            this._praxisTagDel.DeleteByTag(tagName);
            base.Delete(tagName);

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("The deletion could not complete.", e); }
        }
    }
}