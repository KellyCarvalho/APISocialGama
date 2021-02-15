using InstaGama.Application.AppInvite.Input;
using InstaGama.Application.AppInvite.Interfaces;
using InstaGama.Application.AppInvite.Output;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppInvite
{
    public class InviteAppService : IInviteAppService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogged _logged;

        public InviteAppService(IInviteRepository inviteRepository, IUserRepository userRepository, ILogged logged)
        {
            _inviteRepository = inviteRepository;
            _userRepository = userRepository;
            _logged = logged;
        }

        public async Task<Invite> GetByUserAsync(int idUser)
        {
            var userId = _logged.GetUserLoggedId();

            var invites = await _inviteRepository
                                       .GetByUserAsync(userId)
                                       .ConfigureAwait(false);
            if (invites is null)
                return default;
     
            return invites;
        }
    
        public async Task<Invite> InsertAsync(InviteInput inviteInput)
        {
            var userId = _logged.GetUserLoggedId();
            var invite = new Invite(userId, inviteInput.IdUserInvite, inviteInput.Message);

            var userFriend = await _userRepository
                                           .GetByIdAsync(inviteInput.IdUserInvite)
                                           .ConfigureAwait(false);

            if (!invite.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            if(userFriend is null)
            {
                throw new ArgumentException("Este Usuário que está tentando fazer amizade não existe");
            }
            var id = await _inviteRepository
                                    .InsertAsync(invite)
                                    .ConfigureAwait(false);
            invite.SetId(id);
            return invite;

        }
    }
}
