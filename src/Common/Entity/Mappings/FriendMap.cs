using WorldZero.Common.Interface.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity.Mappings
{
    [Table("Friends")]
    public class FriendMap : IIdIdEntityMap
    {
        // this class will implement IIdIdEntityMap, and it will connect the existing IDs with new foriegn keys
        [ForeignKey("LeftId")]
        internal Character LeftFriend { get; set; }

        [ForeignKey("RightId")]
        internal Character RightFriend { get; set; }
    }
}