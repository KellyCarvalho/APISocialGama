using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
   public interface IFriendsRepository
    {

        Task<int> InsertAsync(Friends friends);

        Task<List<Friends>> GetFriendsByUserIdAsync(int userId);
        Task UpdateAsync(int idUser,int idFriend);
        public Task <Friends> GetFriendsByFriendIdAsync(int userId, int friendId);


        public Task<List<Friends>> GetFriendsByFriendPendingAsync(int userId);

        public Task<List<User>> GetProfileAllFriends(int userId);

        public Task <User> GetProfileFriendById(int idUser, int friendId);



        public Task <Friends> GetFriendsByFriendIdPendingAsync(int userId, int friendId);

        public Task<List<Friends>> GetAllFriendAsync(int friendId);

        public Task<List<Friends>> GetAllFriendByIdUserAsync(int userId);

        public Task DeleteAsync(int userId, int idFriend);
        Task DeleteByFriendIdAsync(int id);

        Task DeleteByIdAsync(int id);






    }
}
