using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Interface.Entity
{
    /// <summary>
    /// This entity represents a many-to-many mapping between two entities by
    /// containing their IDs.
    /// </summary>
    /// <typeparam name="LeftValueObject">
    /// The first entity's `ISingleValueObject` to map.
    /// </typeparam>
    /// <typeparam name="LeftIdType">
    /// The built-in of the ID of the first entity.
    /// </typeparam>
    /// <typeparam name="RightValueObject">
    /// The second entity's `ISingleValueObject` to map.
    /// </typeparam>
    /// <typeparam name="RightIdType">
    /// The built-in of the ID of the second entity.
    /// </typeparam>
    /// <remarks>
    /// As usual, enforcing that the combination of the left and right is the
    /// responsiblity of the repo.  
    /// When making an implementation, please have the class name match the
    /// order of the enitites (ie: an implementation that maps left entity
    /// `Praxis` and right entity `Flag` should be named something like
    /// `PraxisFlag`). An exception to this rule is for self-referencial
    /// relations.
    /// </remarks>
    public abstract class IEntityMap
        <LeftValueObject, LeftIdType, RightValueObject, RightIdType> : IIdEntity
        where LeftValueObject  : ISingleValueObject<LeftIdType>
        where RightValueObject : ISingleValueObject<RightIdType>
    {
        // TODO: add stuff to make this work w/ efcore

        [Required]
        public abstract LeftIdType LeftId { get; set; }
        [NotMapped]
        protected LeftValueObject _leftId;

        [Required]
        public abstract RightIdType RightId { get; set; }
        [NotMapped]
        protected RightValueObject _rightId;
    }
}