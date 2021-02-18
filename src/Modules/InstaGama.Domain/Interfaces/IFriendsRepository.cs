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
        public Task<List<Friends>> GetFriendsByFriendIdAsync(int friendId);


        public Task<List<Friends>> GetFriendsByFriendPendingAsync(int friendId);

        public Task <Friends> GetFriendsByFriendIdPendingAsync(int friendId);
        




    }
}
