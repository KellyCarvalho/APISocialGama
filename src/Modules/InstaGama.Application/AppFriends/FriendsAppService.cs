using InstaGama.Application.AppFriends.Input;
using InstaGama.Application.AppFriends.Interfaces;
using InstaGama.Application.AppUser.Output;
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

        public async Task DeleteAsync(int idFriend)
        {
            var userId = _logged.GetUserLoggedId();

            var checkIfExistFriend = await _friendsRepository
                                            .GetFriendsByFriendIdAsync(userId,idFriend)
                                            .ConfigureAwait(false);

            if (checkIfExistFriend != null)
            {
                await _friendsRepository
                        .DeleteAsync(checkIfExistFriend.UserId, checkIfExistFriend.UserFriendId)
                        .ConfigureAwait(false);

                await _friendsRepository
                      .DeleteAsync(checkIfExistFriend.UserFriendId,checkIfExistFriend.UserId)
                      .ConfigureAwait(false);
            }
            if (checkIfExistFriend == null)
            {
                throw new ArgumentException("O usuário que está tentando desfazer a amizade não está na sua lista de amigos");
            }

        }

        public async Task<Friends> GetFriendsByFriendIdAsync(int idfriend)
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetFriendsByFriendIdAsync(userId, idfriend)
                                    .ConfigureAwait(false);
            return friends;
        }

        public async Task<Friends> GetFriendsByFriendIdPendingAsync(int friendId)
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetFriendsByFriendIdPendingAsync(userId,friendId)
                                    .ConfigureAwait(false);
            return friends;
        }

        public async Task<List<Friends>> GetFriendsByFriendPendingAsync()
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

            var checkIfAlredyExist = await _friendsRepository
                                        .GetFriendsByFriendIdAsync(userId,friendsInput.UserFriendId)
                                        .ConfigureAwait(false);

            

            var checkIfIsPending1 = await _friendsRepository
                                            .GetFriendsByFriendIdPendingAsync(userId,friendsInput.UserFriendId)
                                            .ConfigureAwait(false);

            var checkIfIsPending2 = await _friendsRepository
                                            .GetFriendsByFriendIdPendingAsync(friendsInput.UserFriendId, userId)
                                            .ConfigureAwait(false);

            if (userId == friendsInput.UserFriendId)
            {
                throw new ArgumentException("Você está tentando enviar uma solicitação para si mesmo, isso não é permitido");
            }


            if (checkIfAlredyExist != null)
            {
                throw new ArgumentException("Você já é amigo dessa pessoa");
            }

          

            if (checkIfIsPending2 != null)
            {
                throw new ArgumentException("Espere a pessoa aceitar seu convite, pois ainda está pendente");
            }

            if (checkIfIsPending1 != null)
            {
                throw new ArgumentException("Existe uma solicitação a ser aceita por você enviada por esta amiga, apenas aceite o convite");
            }

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
            var checkIfExistAsFriend = await _friendsRepository
                                              .GetFriendsByFriendIdAsync(userId,idFriend)
                                              .ConfigureAwait(false);

            var checkIfPendingExiste = await _friendsRepository
                                                .GetFriendsByFriendIdPendingAsync(userId, idFriend)
                                                .ConfigureAwait(false);

            if(checkIfExistAsFriend!=null)
            {
                throw new ArgumentException("Você já aceitou esta pessoa");
            }

            if(checkIfPendingExiste ==null){
                throw new ArgumentException("Não existe convites pendentes envie um para esta pessoa");
            }

            if(checkIfPendingExiste != null)
            {
                if (checkIfPendingExiste.UserFriendId==userId)
                {
                    

                    var friendshipAcepted = new Friends(userId, idFriend, 0);

                    await _friendsRepository
                          .UpdateAsync(userId, idFriend);

                    await _friendsRepository
                          .InsertAsync(friendshipAcepted)
                          .ConfigureAwait(false);


                    return friendshipAcepted;

                }

         
               
            }

            return default;

        }

        public async Task<List<UserViewModel>> GetProfileAllFriends()
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetProfileAllFriends(userId)
                                    .ConfigureAwait(false);

            var allfriends = new List<UserViewModel>();

            foreach (var amigo in friends)
            {
              var convertUserToView = new UserViewModel()
               {
                   Id = amigo.Id,
                   Name = amigo.Name,
                   Birthday = amigo.Birthday,
                   Email = amigo.Email,
                   Gender = amigo.Gender,
                   Photo = amigo.Photo
               };

                allfriends.Add(convertUserToView);

            }



            return allfriends;
        }

        public async Task<UserViewModel> GetProfileFriendById(int idFriend)
        {
            var userId = _logged.GetUserLoggedId();

            var friends = await _friendsRepository
                                    .GetProfileFriendById(userId,idFriend)
                                    .ConfigureAwait(false);
              return new UserViewModel()
            {
                Id = friends.Id,
                Name = friends.Name,
                Birthday = friends.Birthday,
                Email = friends.Email,
                Gender = friends.Gender,
                Photo = friends.Photo
            };
            
        }


    }
}
