using System.Collections;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class FriendList : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int FriendId { get; set; }
        public ApplicationUser Friend { get; set; }
    }
}