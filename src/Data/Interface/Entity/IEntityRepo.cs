using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Entity
{
    /// <summary>
    /// This is a generic repository for entities that includes CRUD methods.
    /// </summary>
    /// <typeparam name="Entity">
    /// The IEntity implementation that this repo abstracts.
    /// </typeparam>
    /// <typeparam name="IdType">
    /// This is the `ISingleValueObject` implementation that `Entity` uses as
    /// an ID.
    /// </typeparam>
    /// <typeparam name="SVOType">
    /// This is the built-in type behind `IdType`.
    /// </typeparam>
    /// <remarks>
    /// On Save(), entities with an int ID will have that value be set.
    /// </remarks>
    public interface IEntityRepo<Entity, IdType, SVOType>
        : IGenericRepo<Entity, IdType>
        where Entity : IEntity<IdType, SVOType>
        where IdType : ISingleValueObject<SVOType>
    { }
}