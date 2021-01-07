using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    /// <summary>
    /// This service class will handle inserting entities. For more, <see cref=
    /// "IEntityRepo.Insert()"/> and <see cref="IEntityRepo.Save()"/>.
    /// </summary>
    public interface IEntityReg<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        /// <summary>
        /// This will store the supplied entity and save the repo. On failure,
        /// this will throw an exception.
        /// </summary>
        TEntity Register(TEntity e);

        /// <summary>
        /// This will store the supplied entity and save the repo. On failure,
        /// this will throw an exception.
        /// </summary>
        Task<TEntity> RegisterAsync(TEntity e);
    }
}