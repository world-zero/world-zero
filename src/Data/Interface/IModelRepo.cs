using WorldZero.Common.Interface;

namespace WorldZero.Data.Interface
{
    /// <summary>
    /// This is a generic repository for accessing a table of a model from the
    /// injected context. This includes CRUD methods.
    /// </summary>
    /// <typeparam name="T">The IModel implementation that this repo abstracts.
    /// </typeparam>
    /// <remarks>
    /// On Save(), models with an int ID will have that value be set.
    /// </remarks>
    public interface IModelRepo<T> : IGenericRepo<T> where T : IModel { }
}