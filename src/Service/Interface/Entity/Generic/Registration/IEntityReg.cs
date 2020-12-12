using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using WorldZero.Service.Interface.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <summary>
    /// This service class will handle inserting entities. For more, <see cref=
    /// "IEntityRepo.Insert()"/> and <see cref="IEntityRepo.Save()"/>.
    /// </summary>
    public abstract class IEntityReg<TEntity, TId, TBuiltIn>
        : ABCEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public IEntityReg(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual TEntity Register(TEntity e)
        {
            this.AssertNotNull(e, "e");
            try
            {
                this._repo.Insert(e);
                this._repo.Save();
            }
            catch (ArgumentException exc)
            { throw new ArgumentException("Failed to complete a register.", exc); }
            return e;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual async Task<TEntity> RegisterAsync(TEntity e)
        {
            return await Task.Run(() => this.Register(e));
        }
    }
}