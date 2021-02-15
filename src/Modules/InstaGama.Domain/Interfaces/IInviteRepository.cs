﻿using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
   public interface IInviteRepository
    {
        Task<int> InsertAsync(Invite invite);
        Task<Invite> GetByUserAsync(int idUser);
        Task<Invite> GetByIdAsync(int id);
    }
}