using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Application.AppFriends.Output
{
   public class FriendViewModel
    {
       
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Gender { get; private set; }
        public string Photo { get; private set; }
    }
}
