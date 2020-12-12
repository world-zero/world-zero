using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityReg<TEntity, TId, TBuiltIn>
        : ABCEntityService<TEntity, TId, TBuiltIn>,
          IEntityReg<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public ABCEntityReg(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }

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

        public virtual async Task<TEntity> RegisterAsync(TEntity e)
        {
            return await Task.Run(() => this.Register(e));
        }
    }
}