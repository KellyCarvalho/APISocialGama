using InstaGama.Application.AppInvite.Input;
using InstaGama.Application.AppInvite.Output;
using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppInvite.Interfaces
{
   public interface IInviteAppService
    {
        Task<Invite> InsertAsync(InviteInput inviteInput);
        Task<Invite> GetByUserAsync(int idUser);
    }
}
