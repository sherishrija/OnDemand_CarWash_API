﻿using Carwash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public interface IUserRepository
    {
       
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetAsync(int id);
        Task<UserModel> AddAsync(UserModel entity);
        Task<UserModel> DeleteAsync(int id);
        Task<UserModel> UpdateAsync(int id, UserModel entity);
        Task<UserModel> Login(Login login);

    }
}
