using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
   public class Invite
    {
        public int Id { get; private set; }
        public int IdUser { get; private set; }
        public int IdUserInvite { get; private set; }
        public string Message { get; private set; }


        public bool IsValid()
        {

            if (string.IsNullOrEmpty(IdUser.ToString())||
                string.IsNullOrEmpty(IdUserInvite.ToString()))
            {
                return false;
            }

            return true;

        }
    }
}
