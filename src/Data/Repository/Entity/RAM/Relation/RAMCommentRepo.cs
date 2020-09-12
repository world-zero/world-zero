using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ICommentRepo">
    /// <remarks>
    /// As this repo is not a production-level tool (database repos fill that
    /// shoe), this will *not* allow multiple comments from the same character
    /// onto a praxis. This is because the third component of the Comment
    /// identifier, SubmissionCount, is not addressed.
    /// </remarks>
    public class RAMCommentRepo
        : IRAMIdIdRepo<Comment>,
          ICommentRepo
    { }
}