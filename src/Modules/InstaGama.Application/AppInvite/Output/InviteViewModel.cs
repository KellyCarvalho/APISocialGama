using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Application.AppInvite.Output
{
   public class InviteViewModel
    {
        public int Id { get; private set; }
        public int IdUser { get; private set; }
        public int IdUserInvite { get; private set; }
        public string Message { get; private set; }
        public int Status { get; private set; }
    }
}
