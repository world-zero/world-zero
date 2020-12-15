using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    public interface ITagDel
        : IEntityDel<ITag, Name, string>
    { }
}