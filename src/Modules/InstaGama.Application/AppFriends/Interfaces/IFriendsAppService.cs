using InstaGama.Application.AppFriends.Input;
using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppFriends.Interfaces
{
   public interface IFriendsAppService
    {
        Task<Friends> InsertAsync(FriendsInput friendsInput);

        Task<List<Friends>> GetFriendsByUserIdAsync();
        Task<Friends> UpdateAsync(int idfriend);

        public Task<List<Friends>> GetFriendsByFriendPendingIdAsync();

        //public Task<Friends> GetFriendsByFriendIdPendecyAsync(int friendId);

    }
}
