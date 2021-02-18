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

        public async Task<List<Friends>> GetFriendsByFriendPendingIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetFriendsByFriendPendingAsync(userId)
                                    .ConfigureAwait(false);
            return friends;
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

        public async Task<Friends> UpdateAsync(int idFriend)
        {
            var userId = _logged.GetUserLoggedId();

            var checkUserExist= await _userRepository
                                        .GetByIdAsync(idFriend)
                                        .ConfigureAwait(false);

            var checkFriendAlredyAcept = await _friendsRepository
                                                     .GetFriendsByFriendIdAsync(idFriend)
                                                    .ConfigureAwait(false);

            if(string.IsNullOrEmpty(checkUserExist.ToString()))
            {
                throw new ArgumentException("Você está tentando fazer uma amizade com um usuário que não está na nossa base de dados");
            }

            if  (string.IsNullOrEmpty(checkFriendAlredyAcept.ToString()))
            {
                throw new ArgumentException("Você já aceitou este amigo");
            }

            var checkIfIdFriendIsEqualToUserId = _friendsRepository
                                                    .GetFriendsByFriendIdPendingAsync(userId)
                                                    .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(checkIfIdFriendIsEqualToUserId.ToString()))
            {
                var friendAcepted = new Friends(userId, idFriend);

                var receivingTableFriend = new Friends(userId, idFriend,0);

                await _friendsRepository
                       .UpdateAsync(userId,idFriend);

                await _friendsRepository
                            .InsertAsync(receivingTableFriend)
                            .ConfigureAwait(false);

                return friendAcepted;
            }
            else
            {
                return default;
            }

           

          
        }
    }
}
