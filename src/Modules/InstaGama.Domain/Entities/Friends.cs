using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
   public class Friends
    {
        public Friends( int userId, int userFriendId)
        {
           
            UserId = userId;
            UserFriendId = userFriendId;
            Pendency = 1;
        }

        public Friends(int id,int userId, int userFriendId,int pendency)
        {
            Id = id;
            UserId = userId;
            UserFriendId = userFriendId;
            Pendency = pendency;
        }



        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int UserFriendId { get; private set; }
        public int  Pendency { get; private set; }

        public bool IsValid()
        {
            if(string.IsNullOrEmpty(UserId.ToString())||
                string.IsNullOrEmpty(UserFriendId.ToString()))
            {
                return false;
            }
            return true;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
