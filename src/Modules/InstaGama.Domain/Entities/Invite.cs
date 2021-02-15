using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
   public class Invite
    {
        public Invite(int id, int idUser, int idUserInvite, string message, int status)
        {
            Id = id;
            IdUser = idUser;
            IdUserInvite = idUserInvite;
            Message = message;
            Status = status;
        }

        public Invite(int idUser, int idUserInvite, string message, int status)
        {
            IdUser = idUser;
            IdUserInvite = idUserInvite;
            Message = message;
            Status = status;
        }

        public Invite(int idUser, int idUserInvite, string message)
        {
            IdUser = idUser;
            IdUserInvite = idUserInvite;
            Message = message;
            Status = 0;
        }

        public Invite(int idUser, int idUserInvite, int status)
        {
            IdUser = idUser;
            IdUserInvite = idUserInvite;
            Status = status;
        }
        public int Id { get; private set; }
        public int IdUser { get; private set; }
        public int IdUserInvite { get; private set; }
        public string Message { get; private set; }
        public int Status { get; private set; }

  

        public bool IsValid()
        {

            if (string.IsNullOrEmpty(IdUser.ToString())||
                string.IsNullOrEmpty(IdUserInvite.ToString()))
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
