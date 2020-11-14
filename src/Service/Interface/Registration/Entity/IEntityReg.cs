using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Interface.Registration.Entity
{
    public abstract class IEntityReg<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected IEntityReg(IEntityRepo<TEntity, TId, TBuiltIn> repo)
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