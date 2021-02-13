using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
    interface IFriendsRepository
    {
        Task<int> InsertAsync(Friends friends);

        Task<List<Friends>> GetFriendsByUserIdAsync(int userId);
    }
}
