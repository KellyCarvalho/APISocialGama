using InstaGama.Application.AppFriends.Input;
using InstaGama.Application.AppFriends.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppFriends
{
    public class FriendsAppService : IFriendsAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly ILogged _logged;

        public FriendsAppService(IUserRepository userRepository, IFriendsRepository friendsRepository, ILogged logged)
        {
            _userRepository = userRepository;
            _friendsRepository = friendsRepository;
            _logged = logged;
        }

        public async Task<List<Friends>> GetFriendsByUserIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetFriendsByUserIdAsync(userId)
                                    .ConfigureAwait(false);
            return friends;
        }

        public async Task<Friends> InsertAsync(FriendsInput friendsInput)
        {
            var userId = _logged.GetUserLoggedId();
          
            var friend = new Friends(userId,friendsInput.UserFriendId);
         

            if (!friend.IsValid())
            {
            throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");

            }

            var id = await _friendsRepository
                            .InsertAsync(friend)
                            .ConfigureAwait(false);
            friend.SetId(id);
            return friend;
        }
    }
}
