﻿using InstaGama.Application.AppUser.Input;
using InstaGama.Application.AppUser.Output;
using InstaGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppUser.Interfaces
{
    public interface IUserAppService
    {
        Task<UserViewModel> InsertAsync(UserInput input);
        Task<UserViewModel> GetByIdAsync(int id);

        Task<List<string>> GetPhotosUserAsync();

        Task<UserViewModel> UpdateAsync(UserInput  input);

        Task DeleteUserAsync();


    }
}
