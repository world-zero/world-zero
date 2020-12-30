using System;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityUpdate<TEntity, TId, TBuiltIn>
        : ABCEntityService<TEntity, TId, TBuiltIn>,
        IEntityUpdate<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public ABCEntityUpdate(IEntityRepo<TEntity, TId, TBuiltIn> repo)
            : base(repo)
        { }

        /// <summary>
        /// This will handle the boilerplate transaction creating and exception
        /// handling; the supplied `amend` is responsible for casting the
        /// public-method-supplied entity to the protected implementation and
        /// changing the value - and also doing any other validation as
        /// necessary.
        /// </summary>
        /// <param name="amend">
        /// The delegate containing the cast and amendment.
        /// </param>
        /// <param name="e">
        /// The entity to update.
        /// </param>
        /// <param name="serialize">
        /// Whether or not to have a Serialized transaction.
        /// </param>
        /// <remarks>
        /// For an example, check out
        /// <see cref="AbilityUpdate.AmendDescription(IAbility, string)"/>.
        /// </remarks>
        protected void AmendHelper(
            Action amend,
            TEntity e,
            bool serialize=false
        )
        {
            this.AssertNotNull(amend, "amend");
            this.AssertNotNull(e, "e");

            void f()
            {
                try
                { amend(); }
                catch (InvalidCastException e)
                {
                    this.DiscardTransaction();
                    throw new InvalidOperationException("Could not cast, there is an outside implementation being supplied.", e);
                }
                catch (ArgumentException e)
                {
                    // This does not discard since Transaction will take care
                    // of that when catching ArgExc's.
                    throw new ArgumentException("Could not complete the amendment.", e);
                }
                this._repo.Update(e);
            }

            this.Transaction(f, serialize);
        }
    }
}