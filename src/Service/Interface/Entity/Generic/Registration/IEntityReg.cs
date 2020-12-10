using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Registration
{
    /// <summary>
    /// This service class will handle inserting entities. For more, <see cref=
    /// "IEntityRepo.Insert()"/> and <see cref="IEntityRepo.Save()"/>.
    /// </summary>
    public abstract class IEntityReg<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : IUnsafeEntity<TId, TBuiltIn>
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