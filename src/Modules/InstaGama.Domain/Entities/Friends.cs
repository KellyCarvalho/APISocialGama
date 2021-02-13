using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
    public class Friends
    {
        public Friends(int userId, int userFriendId, bool pendency)
        {
            UserId = userId;
            UserFriendId = userFriendId;
            Pendency = pendency;
        }
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int UserFriendId { get; private set; }
        public bool Pendency { get; private set; }
    }
}





