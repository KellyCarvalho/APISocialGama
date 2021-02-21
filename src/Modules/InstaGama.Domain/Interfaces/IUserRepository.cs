using InstaGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(User user);
        Task<User> GetByLoginAsync(string login);
        Task<User> GetByIdAsync(int id);
        Task<List<string>> GetPhotosUserAsync(int userId);

        Task UpdateAsync(User user, int idUser);
        Task DeleteAsync( int idUser);
        Task<List<User>> GetAllUsersAsync();




    }
}
