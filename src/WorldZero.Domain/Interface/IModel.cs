namespace WorldZero.Domain.Interface
{
    /// <remarks>
    /// At some point, it might be nice to loosely couple each model with their
    /// associated entity, but there is also a strong argument for keeping
    /// these classes tightly coupled as the entity controllers of the models
    /// really need most, if not all, of the information about the model, so
    /// abstracting them away could easily get dicey. That and EF Core is not a
    /// fan of Code First.
    /// </remarks>
    public interface IModel
    {
        //IModel ShallowCopy();
    }
}