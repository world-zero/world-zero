using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary
{
    /// <summary>
    /// This is the abstraction for an entity DTO, which has no creation
    /// restrictions, but is rarely, if ever, used in system services.
    /// </summary>
    public interface IEntityDTO<TId, TBuiltIn> : IDTO
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        /// <remarks>
        /// This is a getter/setter in order to play nice with repos.
        /// </remarks>
        TId Id { get; set; }

        /// <summary>
        /// Unless you are implementing a whole new entity or you are working
        /// on the RAMEntityRepos, you can safely ignore this method.
        /// <br />
        /// This method will return a list of sets, each of which contains
        /// at least one member that a repository should ensure are unique as a
        /// combiniation, per set. This does not include the Id of an entity.
        /// </summary>
        /// <returns>
        /// A list of HashSets of ISingleValueObjects and/or built in types
        /// that repos must consider treat as unique for a specific instance.
        /// These types will be able to cast to object and have .Equals work
        /// appropriately. This will never return null, but it can return an
        /// empty list.
        /// </returns>
        W0List<W0Set<object>> GetUniqueRules();
    }
}