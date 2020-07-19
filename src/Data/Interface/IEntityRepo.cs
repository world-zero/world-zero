using WorldZero.Common.Interface;

namespace WorldZero.Data.Interface
{
    /// <summary>
    /// This is a generic repository for entities that includes CRUD methods.
    /// </summary>
    /// <typeparam name="T">The IEntity implementation that this repo abstracts.
    /// </typeparam>
    /// <remarks>
    /// On Save(), entities with an int ID will have that value be set.
    /// </remarks>
    public interface IEntityRepo<T> : IGenericRepo<T> where T : IEntity { }
}