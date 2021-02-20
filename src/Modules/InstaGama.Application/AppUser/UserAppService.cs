using InstaGama.Application.AppUser.Input;
using InstaGama.Application.AppUser.Interfaces;
using InstaGama.Application.AppUser.Output;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppUser
{
    public class UserAppService : IUserAppService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogged _logged;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IPostageRepository _postageRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikesRepository _likesRepository;

        public UserAppService(IGenderRepository genderRepository, IUserRepository userRepository, 
                                                ILogged logged, IFriendsRepository friendsRepository, 
                                                IPostageRepository postageRepository,ICommentRepository commentRepository,
                                                ILikesRepository likesRepository)
        {
            _genderRepository = genderRepository;
            _userRepository = userRepository;
            _logged = logged;
            _friendsRepository = friendsRepository;
            _postageRepository = postageRepository;
            _commentRepository = commentRepository;
            _likesRepository = likesRepository;
        }

        public async Task DeleteUserAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var checkIfUserExist = await _userRepository
                            .GetByIdAsync(userId)
                            .ConfigureAwait(false);

           
            

            
         
            

            if (checkIfUserExist != null)
            {


                await _likesRepository
                               .DeleteAsyncByUser(userId)
                               .ConfigureAwait(false);

                await _commentRepository
                         .DeleteByIdUserAsync(userId)
                         .ConfigureAwait(false);

                await _postageRepository
                          .DeleteAsync(userId)
                          .ConfigureAwait(false);
                





                await   _friendsRepository
                        .DeleteByFriendIdAsync(userId)
                        .ConfigureAwait(false);
                await _friendsRepository
                        .DeleteByIdAsync(userId)
                        .ConfigureAwait(false);


                await _userRepository
                            .DeleteAsync(userId)
                            .ConfigureAwait(false);

               






            }
            else
            {
                throw new ArgumentException("Pessoa não localizada");
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _userRepository
                                   .GetAllUsersAsync()
                                   .ConfigureAwait(false);

            var allUsers = new List<UserViewModel>();

         

            return users;

           
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user = await _userRepository
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (user is null)
                return default;

            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Photo = user.Photo
            };
        }

        public async Task<List<string>> GetPhotosUserAsync()
        {
            var userLogged = _logged.GetUserLoggedId();
            var photos = await _userRepository
                                .GetPhotosUserAsync(userLogged)
                                .ConfigureAwait(false);

            if (photos is null)
                return default;

            return photos;

        }

        public async Task<UserViewModel> InsertAsync(UserInput input)
        {
            var gender = await _genderRepository
                                   .GetByIdAsync(input.GenderId)
                                   .ConfigureAwait(false);

            var userAlredyExist = await _userRepository
                                        .GetByLoginAsync(input.Email)
                                        .ConfigureAwait(false);

            if(userAlredyExist != null)
            {
                throw new ArgumentException("Já existe um usuário com este email, tente outro!");
            }

            if (gender is null)
            {
                throw new ArgumentException("O genero que está tentando associar ao usuário não existe!");
            }

            var user = new User(input.Email,
                                 input.Password,
                                 input.Name,
                                 input.Birthday,
                                 new Gender(gender.Id, gender.Description),
                                 input.Photo);
          
            if (!user.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            var id = await _userRepository
                                .InsertAsync(user)
                                .ConfigureAwait(false);

            return new UserViewModel()
            {
                Id = id,
                Name = user.Name,
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Photo = user.Photo
            };
        }

        public async Task<UserViewModel> UpdateAsync(UserInput input)
        {
            var userId = _logged.GetUserLoggedId();

            var gender = await _genderRepository
                            .GetByIdAsync(input.GenderId);

       

            var userUpdated = new User(input.Email,input.Password,input.Name,input.Birthday, gender, input.Photo);

            await _userRepository
                 .UpdateAsync(userUpdated, userId)
                 .ConfigureAwait(false);

            var checkIfUserIsSameLogged = await _userRepository
                                            .GetByIdAsync(userId)
                                            .ConfigureAwait(false);

            if (checkIfUserIsSameLogged.Id == userId)
            {
                return new UserViewModel()
                {
                    Id = userId,
                    Name = userUpdated.Name,
                    Birthday = userUpdated.Birthday,
                    Email = userUpdated.Email,
                    Gender = userUpdated.Gender,
                    Photo = userUpdated.Photo
                };

               
            }


            return default;

        }
    }
}
