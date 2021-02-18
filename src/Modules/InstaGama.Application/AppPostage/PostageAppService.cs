using InstaGama.Application.AppPostage.Input;
using InstaGama.Application.AppPostage.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppPostage
{
    public class PostageAppService : IPostageAppService
    {
        private readonly IPostageRepository _postageRepository;
        private readonly ILogged _logged;
        private readonly IFriendsRepository _friendsRepository;
        public PostageAppService(IPostageRepository postageRepository,
                                  ILogged logged, IFriendsRepository friendsRepository)
        {
            _postageRepository = postageRepository;
            _logged = logged;
            _friendsRepository = friendsRepository;
        }

        public async Task<List<Postage>> GetPostageFriendAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var postagesFriends = await _postageRepository
                                    .GetPostageFriendAsync(userId)
                                    .ConfigureAwait(false);
            return postagesFriends;
        }

        public async Task<List<Postage>> GetPostageByUserIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var postages = await _postageRepository
                                    .GetPostageByUserIdAsync(userId)
                                    .ConfigureAwait(false);
            return postages;
        }

        public async Task<Postage> InsertAsync(PostageInput input)
        {
            var userId = _logged.GetUserLoggedId();

            var postage = new Postage(input.Text, userId,input.Photo);
            if (!postage.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            



            var id = await _postageRepository
                             .InsertAsync(postage)
                             .ConfigureAwait(false);

            postage.SetId(id);

            return postage;
        }

        public async Task<List<Postage>> GetPostageFriendIdAsync(int idFriend)
        {
            var userId = _logged.GetUserLoggedId();
            var checkIfAlredyFriend = _friendsRepository
                                        .GetFriendsByFriendIdAsync(userId,idFriend)
                                        .ConfigureAwait(false);


            var postageFriend = await _postageRepository
                                    .GetPostageByUserIdAsync(idFriend)
                                    .ConfigureAwait(false);

            if (string.IsNullOrEmpty(checkIfAlredyFriend.ToString()))
            {
                throw new ArgumentException("Você Não tem permissão para ver as postagens deste usuário, envie uma solicitação de amizade");

            }

            if (!string.IsNullOrEmpty(postageFriend.ToString()))
            {
                return postageFriend;
            }

            if (string.IsNullOrEmpty(postageFriend.ToString()))
            {
                throw new ArgumentException("Este usuário ainda não tem postagem");
            }


            return default;
        }
    }
}
