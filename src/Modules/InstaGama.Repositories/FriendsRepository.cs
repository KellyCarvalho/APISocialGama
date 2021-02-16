using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class FriendsRepository : IFriendsRepository
    {
        public Task<List<Friends>> GetFriendsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Friends friends)
        {
            throw new NotImplementedException();
        }
    }
}
