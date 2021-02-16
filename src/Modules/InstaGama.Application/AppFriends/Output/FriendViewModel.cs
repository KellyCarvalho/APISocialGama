using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Application.AppFriends.Output
{
   public class FriendViewModel
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int UserFriendId { get; private set; }
        public int Pendency { get; private set; }
    }
}
